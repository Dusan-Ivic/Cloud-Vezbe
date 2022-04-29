﻿using Common;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankServer
    {
        private ServiceHost serviceHost;
        private string internalEndpointName = "InternalRequest";

        public BankServer()
        {
            RoleInstanceEndpoint internalEndpoint
                = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[internalEndpointName];
            string endpoint = String.Format("net.tcp://{0}/{1}", internalEndpoint.IPEndpoint, internalEndpointName);
            serviceHost = new ServiceHost(typeof(BankServerProvider));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IBank), binding, endpoint);
        }

        public void Open()
        {
            try
            {
                serviceHost.Open();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type opened successfuly at {1}.", internalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host open error for {0} endpoint type. Error message is: {1}.", internalEndpointName, e.Message);
            }
        }

        public void Close()
        {
            try
            {
                serviceHost.Close();
                Trace.TraceInformation(String.Format("Host for {0} endpoint type closed successfuly at {1}.", internalEndpointName, DateTime.Now));
            }
            catch (Exception e)
            {
                Trace.TraceInformation("Host close error for {0} endpoint type. Error message is: {1}.", internalEndpointName, e.Message);
            }
        }
    }
}
