using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace R.Helpers
{
    public enum LogDetailLevel
    {
        None,
        Minimum,
        Normal,
        High
    }
    
    public class LogFacility : IDisposable
    {
        string m_FileName;
        LogDetailLevel m_LogDetailLevel;

        public LogFacility(string FileName)
            :this(FileName, LogDetailLevel.Normal)
        {
        }

        public LogFacility(string FileName, LogDetailLevel LogDetailLevel)
        {
            m_FileName = FileName;
            m_LogDetailLevel = LogDetailLevel;
            //AddEvent("Application has started. Version: " + Assembly.GetExecutingAssembly().GetName().Version, LogDetailLevel.None);
        }

        public void AddEvent(string Message, LogDetailLevel EventLevel)
        {
#if DEBUG
            //global::System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " " + Message);
#endif
            if (EventLevel > m_LogDetailLevel)
                return;
            string delimiter = "";
            if (m_FileName.IndexOf(".htm") != -1)
                delimiter = "<br>";
            try
            {
                StreamWriter writer = new StreamWriter(m_FileName, true);
                writer.WriteLine(DateTime.Now.ToString() + " " + Message + delimiter);
                writer.Close();
            }
            catch (Exception)
            {
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.AddEvent("Application is ending.", LogDetailLevel.None);
        }

        #endregion
    }
}
