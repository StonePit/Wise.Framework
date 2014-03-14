﻿using System;
using System.Diagnostics;
using System.Windows;
using Common.Logging;
using Wise.Framework.DependencyInjection;
using Wise.Framework.Interface.ExceptionHandling;

namespace Wise.Framework.Presentation
{
    /// <summary>
    /// Application class
    /// </summary>
    public class WiseApplication : Application
    {
        private readonly ILog log = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Creates new instance of WiseApplication
        /// </summary>
        public WiseApplication()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
        }

        /// <summary>
        /// Handler listening for Unhandled Exception
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">exception argument</param>
        protected void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// Catch exception on event
        /// </summary>
        /// <param name="exception"> exception object</param>
        protected void HandleException(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            if (!Current.Dispatcher.CheckAccess())
            {
                Current.Dispatcher.BeginInvoke((Action<Exception>)HandleException, exception);
            }
            else
            {
                try
                {
                    LogUnhandledException(exception);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error while attempting to log unhanded exception " + ex);
                    HandleStartupException(exception);
                }
                if (Container.IsInitialised)
                {
                    if (Container.Current.IsTypeRegistered<IExceptionService>())
                    {
                        var exceptionService = Container.Current.Resolve<IExceptionService>();
                        exceptionService.ShowDialog(exception, ExceptionOptions.ExitOrContinue);
                    }
                    else
                    {
                        HandleStartupException(exception);
                    }
                }
                else
                {
                    HandleStartupException(exception);
                }

            }
        }

        /// <summary>
        /// Method responsible for logging unhandled exception in logger
        /// </summary>
        /// <param name="exception">exception which occur</param>
        protected virtual void LogUnhandledException(Exception exception)
        {
            log.Error(string.Format("unhandled exception occurred {0} : \n\r {1}", exception.Message, exception.StackTrace), exception);
        }

        /// <summary>
        /// Helper method for handling startup exceptions.
        /// </summary>
        /// <param name="exception"> exception</param>
        private static void HandleStartupException(Exception exception)
        {
            var message = string.Format("The Following unhanded exception occurred on startup: {0}{1}", Environment.NewLine, exception);
            
            MessageBox.Show(message);
        }
    }
}
