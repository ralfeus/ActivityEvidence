using System;
using ActivityEvidence.EvidenceService;
using System.Collections;

namespace ActivityEvidence
{
	public enum ModificationStatus
	{
		None,
		Modified,
		Deleted
	}
	
	/// <summary>
	/// Activity class is designed for storing activity data from the server and additional data for manipulating activities at the client side
	/// </summary>
	public class ActivityPresentation
	{
		private ActivityReportStruct _entry;
		private ClaimStruct _claim;
		private ModificationStatus _modified;
		private Hashtable _oldValues = new Hashtable();

		/// <summary>
		/// A data storing at the server and taken using Web Service method
		/// </summary>
		public ActivityReportStruct Entry
		{
			get { return _entry; }
			set 
			{
				_entry = value; 
				_claim.Activity = value;
			}
		}

		/// <summary>
		/// A claim code data
		/// </summary>
		public ClaimStruct Claim
		{
			get { return _claim; }
			set { _claim = value; }
		}
		
		/// <summary>
		/// A flag showing whether an activity was modified or deleted on the client side
		/// </summary>
		public ModificationStatus Modified
		{
			get {return _modified; }
			set { _modified = value; }
		}

		public Hashtable OldValues
		{
			get { return _oldValues; }
		}
		
		/// <summary>
		/// Constructor for creation a new instance of the Activity. 
		/// </summary>
		/// <param name="Entry">A data taken from the server</param>
		public ActivityPresentation(ActivityReportStruct Entry)
		{
			_claim = new ClaimStruct();
			_entry = Entry;
			_claim.Activity = Entry;
			_modified = ModificationStatus.None;
		}

		/// <summary>
		/// Constructor for creation a new instance of the activity presentation by claim
		/// </summary>
		/// <param name="Claim">A claim data taken from the server</param>
		public ActivityPresentation(ClaimStruct Claim)
		{
			_claim = Claim;
			_entry = _claim.Activity;
			_modified = ModificationStatus.None;
		}
	}
}
