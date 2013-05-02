using System;
using System.Collections;
using ActivityEvidence.EvidenceService;

namespace ActivityEvidence
{
	// WeekDayHours classes	
	public class WeekDayHoursKeysCollection : CollectionBase
	{
		public DayOfWeek this[int index]
		{
			get { return (DayOfWeek)List[index]; }
			set { List[index] = value; }
		}

		public int Add(DayOfWeek Key)
		{
			return List.Add(Key);
		}
	}
	
	public class WeekDayHoursDictionary : DictionaryBase
	{
		public double this[DayOfWeek index]
		{
			get { return (double)Dictionary[index]; }
			set { Dictionary[index] = value; }
		}

		public WeekDayHoursKeysCollection Keys
		{
			get 
			{ 
				WeekDayHoursKeysCollection keys = new WeekDayHoursKeysCollection();
				foreach (object key in Dictionary.Keys)
					keys.Add((DayOfWeek)key);
				return keys; 
			}
		}

		public WeekDayHoursDictionary()
		{
			Dictionary.Add(DayOfWeek.Sunday, (double)0);
			Dictionary.Add(DayOfWeek.Monday, (double)0);
			Dictionary.Add(DayOfWeek.Tuesday, (double)0);
			Dictionary.Add(DayOfWeek.Wednesday, (double)0);
			Dictionary.Add(DayOfWeek.Thursday, (double)0);
			Dictionary.Add(DayOfWeek.Friday, (double)0);
			Dictionary.Add(DayOfWeek.Saturday, (double)0);
		}

//		public void Add(DayOfWeek Index, float Value)
//		{
//			Dictionary.Add(Index, Value);
//		}
		
//		public bool Contains(DayOfWeek Index)
//		{
//			return Dictionary.Contains(Index);
//		}
	}

	// ClaimMatrix classes
	public class ClaimMatrixKeysCollection : CollectionBase
	{
		public ClaimCode this[int index]
		{
			get { return (ClaimCode)List[index]; }
			set { List[index] = value; }
		}

		public int Add(ClaimCode Key)
		{
			return List.Add(Key);
		}

//		public void Sort()
//		{
//			ClaimMatrixKeysCollection tmpColl = new ClaimMatrixKeysCollection();
//			while (this.Count != 0)
//			{
//				ClaimCode minClaimCode = this[0];
//				foreach (ClaimCode code in this)
//					if (code.ToString().CompareTo(minClaimCode.ToString()) < 0)
//						minClaimCode = code;
//				tmpColl.Add(minClaimCode);
//				this.List.Remove(minClaimCode);
//			}
//			foreach (ClaimCode code in tmpColl)
//				this.Add(code);
//		}
	}
	
	public class ClaimMatrixDictionary : DictionaryBase
	{
		public WeekDayHoursDictionary this[ClaimCode index]
		{
			get { return (WeekDayHoursDictionary)Dictionary[index]; }
			set { Dictionary[index] = value; }
		}

		public ClaimMatrixKeysCollection Keys
		{
			get 
			{ 
				ClaimMatrixKeysCollection keys = new ClaimMatrixKeysCollection();
				foreach (object key in Dictionary.Keys)
					keys.Add((ClaimCode)key);
				return keys; 
			}
		}

		public void Add(ClaimCode Index)
		{
			Dictionary.Add(Index, new WeekDayHoursDictionary());
		}

		public bool Contains(ClaimCode Index)
		{
			foreach (object key in Dictionary.Keys)
				if (((ClaimCode)key).Equals(Index))
					return true;
			return false;
		}
	}
}