using System;
using System.Collections.Generic;
using System.Configuration;
using DAL.Nhibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DAL.NhibernateBase
{
    /// <summary>
    /// A Singleton that creates and persits a single SessionFactory for the to program to access globally.
    /// This uses the .Net CallContext to store a session for each thread.
    /// 
    /// This is heavely based on 'NHibernate Best Practices with ASP.NET' By Billy McCafferty.
    /// http://www.codeproject.com/KB/architecture/NHibernateBestPractices.aspx
    /// </summary>
    public class NHibernateSessionManager : INHibernateSessionManager
    {
        #region Static Content
        private static INHibernateSessionManager _nHibernateSessionManager = null;

        /// <summary>
        /// Set method is exposed so that the INHibernateSessionManager can be swapped out for Unit Testing.
        /// NOTE: Cannot set Instance after it has been initialized, and calling Get will automatically intialize the Instance.
        /// </summary>
        public static INHibernateSessionManager Instance
        {
            get
            {
                if (_nHibernateSessionManager == null)
                    _nHibernateSessionManager = new NHibernateSessionManager();
                return _nHibernateSessionManager;
            }
            set
            {
                if (_nHibernateSessionManager != null)
                    throw new Exception("Cannot set Instance after it has been initialized.");
                _nHibernateSessionManager = value;
            }
        }
        #endregion

        #region Declarations
        private static IDictionary<string, ISessionFactory> _allFactories = Initialize();
        private static IDictionary<string, ISessionFactory> Initialize()
        {
            IDictionary<string, ISessionFactory> dictionary = new Dictionary<string, ISessionFactory>();
            return dictionary;
        }


        private ISessionFactory _sessionFactory;
        private const string SessionContextKey = "NHibernateSession-ContextKey";
        #endregion

        #region Constructors & Finalizers

        /// <summary>
        /// This will load the NHibernate settings from the App.config.
        /// Note: This can/should be expanded to support multiple databases.
        /// </summary>
        private NHibernateSessionManager()
        {
            //Old Xml Map
            //_sessionFactory = new Configuration().Configure().BuildSessionFactory();

            string conStr = ConfigurationManager.ConnectionStrings["base"].ConnectionString;
            string test = ConfigurationManager.AppSettings["Test"];


            if (!string.IsNullOrEmpty(test)
                && test == "1")
            {

                _sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration
                                .Standard.InMemory().ShowSql()
                                .ConnectionString(
                        "Data Source=:memory:;Version=3;New=True;Pooling=True;Max Pool Size=1;")
                    ).Mappings(Mapping)
                    .ExposeConfiguration
                    (cfg => 
                              new SchemaExport(cfg)
                                .Create(false, false) 
                    )
                    .BuildSessionFactory();
            }else
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                                  .ConnectionString(
                                      conStr
                                  )
                                  .ShowSql()
                    ).Mappings(Mapping)
                    .ExposeConfiguration
                        (cfg => new SchemaUpdate(cfg)
                                    .Execute(true, true)
                        )
                    .BuildSessionFactory(); 
            }
        }
        ~NHibernateSessionManager()
        {
            Dispose(true);
        }
        #endregion

        #region IDisposable

        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(false);
        }
        private void Dispose(bool finalizing)
        {
            if (!_isDisposed
                && _sessionFactory != null)
            {
                // Close SessionFactory
                _sessionFactory.Close();
                _sessionFactory.Dispose();

                foreach (KeyValuePair<string, ISessionFactory> pair in _allFactories)
                {
                    pair.Value.Close();
                    pair.Value.Dispose();
                }

                // Flag as disposed.
                _isDisposed = true;
                if (!finalizing)
                    GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Methods

        public INHibernateSession CreateSession()
        {
            return new NHibernateSession(CreateISession());
        }

        public ISession CreateISession()
        {
            ISession iSession;
            lock (_sessionFactory)
            {
                iSession = _sessionFactory.OpenSession();
            }
            return iSession;
        }

        #endregion

        #region Properties
        public INHibernateSession Session
        {
            get
            {
                INHibernateSession session = ContextSession;

                // If the thread does not yet have a session, create one.
                if (session == null)
                {
                    session = CreateSession();

                    // Save to CallContext.
                    ContextSession = session;
                }

                return session;
            }
        }
        private INHibernateSession ContextSession
        {
            get
            {
                if (IsWebContext)
                    return (NHibernateSession)System.Web.HttpContext.Current.Items[SessionContextKey];
                return (NHibernateSession)System.Runtime.Remoting.Messaging.CallContext.GetData(SessionContextKey);
            }
            set
            {
                if (IsWebContext)
                    System.Web.HttpContext.Current.Items[SessionContextKey] = value;
                else
                    System.Runtime.Remoting.Messaging.CallContext.SetData(SessionContextKey, value);
            }
        }
        private bool IsWebContext
        {
            get { return (System.Web.HttpContext.Current != null); }
        }

        #endregion

        /// <summary>
        /// Map the models necessary
        /// </summary>
        /// <param name="mapping"></param>
        private void Mapping(MappingConfiguration mapping)
        {
            var cfg = new ConfigAutoMap();
            /*mapping.AutoMappings
                .Add(AutoMap.AssemblyOf<Usuario>(cfg))
                .Add(AutoMap.AssemblyOf<Projeto>(cfg))
                .Add(AutoMap.AssemblyOf<PermissaoUsuario>(cfg))
                .Add(AutoMap.AssemblyOf<Marco>(cfg))
                .Add(AutoMap.AssemblyOf<DeclaracaoEscopo>(cfg))
                .Add(AutoMap.AssemblyOf<ControleRevisao>(cfg))
                .Add(AutoMap.AssemblyOf<LicoesAprendidas>(cfg))
                .Add(AutoMap.AssemblyOf<Predecessor>(cfg))
                .Add(AutoMap.AssemblyOf<Tarefa>(cfg))
                ;*/
        }
    }
}