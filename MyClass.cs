using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Linq;

using Microsoft.Crm.Sdk.Messages;

using System.Threading;
using System.Threading.Tasks;

using avikcompany_dll;

namespace ultimate_plugin
{

	public class MyClass:IPlugin
	{
		static IPluginExecutionContext context;
		static IOrganizationServiceFactory factory;
		static IOrganizationService service;
		static IServiceProvider provider;
		static bool context_complete=false;
		static bool factory_complete=false;
		static bool service_complete=false;


		static Lead lead = new Lead();

		void Execute(IServiceProvider serviceProvider)
		{
			provider = serviceProvider;
			ThreadStart getcontext_ = new ThreadStart(getcontext);
			ThreadStart getfactory_ = new ThreadStart(getfactory);
			ThreadStart getservice_ = new ThreadStart(getservice);
			Thread context_ = new Thread(getcontext_);
			Thread factory_ = new Thread(getfactory_);
			Thread service_ = new Thread(getservice_);
			context_.Priority = ThreadPriority.Normal;
			factory_.Priority = ThreadPriority.Normal;
			context_.Start ();
			factory_.Start ();
			while (context_complete!=true&&factory_complete!=true)
			{

			}


		}


		static void getcontext()
		{
			try
			{
				context = (IPluginExecutionContext)provider.GetService(typeof(IPluginExecutionContext));
				lead =(avikcompany_dll.Lead)context.InputParameters["Target"];

			}
			catch(Exception exp)
			{
				context_complete = false;
				Thread.CurrentThread.Abort();

			}
			context_complete = true;
		}
		static void getfactory()
		{
			try
			{
				factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
			}
			catch(Exception exp)
			{
				factory_complete = false;
				Thread.CurrentThread.Abort();
			}
			factory_complete = true;
		}
		static void getservice()
		{
			try
			{
				service = factory.CreateOrganizationService(context.UserId);
			}
			catch (Exception exp)
			{
				service_complete = false;
				Thread.CurrentThread.Abort();

			}
			service_complete = true;

		}

		static void retrive_duplicate_prevention_rule()
		{

		}

		static void qualify_lead()
		{

		}

		static void if_create()
		{

		}
		static void if_update()
		{

		}
		static void if_assign()
		{

		}
	}
}

