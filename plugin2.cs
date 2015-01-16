using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;



using crm_ddl_namespace;

namespace plugin2
{
    public class Class1:IPlugin
    {
        Entity b_22;
        nazi_b_23_entity b_23 = new nazi_b_23_entity();
        ColumnSet c= new ColumnSet(true);
        IPluginExecutionContext context;
        IOrganizationServiceFactory factory;
        IOrganizationService service;
        SetStateRequest setstate = new SetStateRequest();
        IServiceProvider serviceProvider_;  
        public void Execute(IServiceProvider serviceProvider)
        {

             serviceProvider_ = serviceProvider;
             initiate(); 
             cal_create();
             cal_change_state();
         

        }

        private async void initiate()
        {
            await init_();
        }

        private async System.Threading.Tasks.Task init_()
        {
            context = (IPluginExecutionContext)serviceProvider_.GetService(typeof(IPluginExecutionContext));
            factory = (IOrganizationServiceFactory)serviceProvider_.GetService(typeof(IOrganizationServiceFactory));
            service = factory.CreateOrganizationService(context.UserId);
            b_22 = (Entity)context.InputParameters["Target"];
            b_23.nazi_name = (string)b_22["nazi_name"];
            b_23.OwnerId = (EntityReference)b_22["ownerid"];
        }

        private async void cal_change_state()
        {
            await cal_change_stat();
        }
        public async System.Threading.Tasks.Task cal_change_stat()
        {
            setstate.EntityMoniker = new EntityReference(b_22.LogicalName,b_22.Id);
            //setstate.State = new OptionSetValue((int)IncidentState.Canceled);
            ////setstate.Status=new OptionSetValue()
            setstate.State = new OptionSetValue(1);
            setstate.Status = new OptionSetValue(2);
            service.Execute(setstate);
        }

        private async void cal_create()
        {
            await create_();
        }

        public async System.Threading.Tasks.Task create_()
        {

           Guid b_23_id = service.Create(b_23);
        }

    }
}
