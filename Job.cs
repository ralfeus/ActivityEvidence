using System;
using ActivityEvidence.EvidenceService;
using System.Collections;
using R.Helpers;

namespace ActivityEvidence
{
	public struct TimePeriod
	{
		public DateTime WorkStart;
		public TimeSpan WorkDuration;
	}

	/// <summary>
	/// Job presents some certain job. Job can consist of several periods
	/// </summary>
	public class Job
	{
		private Settings _settings;
		private ActivitiesEvidence _service;
		private SortedList _timePeriods;
		private DateTime _lastWorkPeriodStart;
		private string _label;

		private ObjectStruct _customer;
		private ObjectStruct _activity;
		private bool _hasTicket;
		private string _ticket;
		private string _comment;

		public string Label
		{
			set {_label = value;}
		}

		public Job(Settings Settings, ActivitiesEvidence Service, ObjectStruct Customer, ObjectStruct Activity, DateTime StartTime)
		{
			_timePeriods = new SortedList(1);
			_settings = Settings;
			_service = Service;
			_customer = Customer;
			_activity = Activity;
			_lastWorkPeriodStart = StartTime;
		}

		public void Pause()
		{
			TimeSpan workDuration = DateTime.Now - _lastWorkPeriodStart;
			_timePeriods.Add(_lastWorkPeriodStart, workDuration);
		}

		public void Resume()
		{
			_lastWorkPeriodStart = DateTime.Now;
		}

		public void Stop()
		{
			this.Pause();
		}

		public void Submit()
		{
			foreach (DictionaryEntry period in _timePeriods)
			{
				_service.BeginSendActivity(
					_settings["AdminID"],
					_customer.ObjectID,
					_activity.ObjectID,
					(DateTime)period.Key,
					(int)period.Value,
					_hasTicket,
					_ticket,
					_comment,
                    0,
					null, null);
			}
		}

		public override string ToString()
		{
			if (_label == null)
				return _customer.ObjectName + " - " + _activity.ObjectName;
			else 
				return _label;
		}
	}
}
