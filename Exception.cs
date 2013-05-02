using System;

namespace ActivityEvidence.Exceptions
{
	/// <summary>
	/// Thrown when invalid data is added
	/// </summary>
	public class DataValidationException : System.Exception
	{
		public DataValidationException(string message)
			:base(message)
		{}
	}

	/// <summary>
	/// Thrown when connection to the server failed
	/// </summary>
	public class ConnectionException : System.Exception
	{
		public ConnectionException(string message)
			:base(message)
		{}
	}
}
