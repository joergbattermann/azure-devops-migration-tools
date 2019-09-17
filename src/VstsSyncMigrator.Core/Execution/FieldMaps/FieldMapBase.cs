﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace VstsSyncMigrator.Engine.ComponentContext
{
    public abstract class FieldMapBase : IFieldMap
    {

        public void Execute(WorkItem source, WorkItem target)
        {
            try
            {
                InternalExecute(source, target);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("  [ERROR] {0}", ex.Message));
                Telemetry.Current.TrackException(ex,
                       new Dictionary<string, string> {
                            { "Source", source.Id.ToString() },
                            { "Target",  target.Id.ToString()}
                       });
                Trace.TraceError($"  [EXCEPTION] {ex}");
            }            
        }
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public abstract string MappingDisplayName { get; }

        internal abstract void InternalExecute(WorkItem source, WorkItem target);
    }
}
