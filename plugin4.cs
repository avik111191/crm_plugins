using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Linq;

using crm_ddl_namespace;

namespace plugin4
{
    public class Class4 : IPlugin
    {
        double num1, num2, sum, mul, div;
        bool div_by_zero=false;
        IPluginExecutionContext context;
        IOrganizationServiceFactory factory;
        IOrganizationService service;
        IServiceProvider Provider;
        nazi_c_22_entity c_22 = new nazi_c_22_entity();
        Entity entity = null;
        public void Execute(IServiceProvider serviceProvider)
        {
            Provider = serviceProvider;
            call_ini_funct();
            call_read();
            if (div_by_zero == true)
            {
                throw new InvalidPluginExecutionException("mult= (num1+num2)*num1= 0 ... so div = num1/0 = undefined ");
            }
            else
            call_write();
        }

       
        private async void call_ini_funct()
        {
            await _int_funct();
        }

        private async System.Threading.Tasks.Task _int_funct()
        {
            context = (IPluginExecutionContext)Provider.GetService(typeof(IPluginExecutionContext));
            factory = (IOrganizationServiceFactory)Provider.GetService(typeof(IOrganizationServiceFactory));
            service = factory.CreateOrganizationService(context.UserId);

        }

        private async void call_read()
        {
            await _read();
        }

        private async System.Threading.Tasks.Task _read()
        {
            
            entity = (Entity)context.InputParameters["Target"];
            c_22.Id = (Guid)entity.Id;
            c_22.nazi_name = (string)entity["nazi_name"];
            c_22.nazi_num1 = num1 = (double)entity["nazi_num1"];
            c_22.nazi_num2 = num2 = (double)entity["nazi_num2"];
          c_22.nazi_sum = sum = num1 + num2;
          c_22.nazi_mult = mul = num1 * sum;
            if(mul==0.00)
             div_by_zero = true;
            else
                c_22.nazi_div_res = div = num1 / mul;
            c_22.OwnerId = (EntityReference)entity["ownerid"];
        }
       
        private async void call_write()
        {
           await _write();
        }

        private async System.Threading.Tasks.Task _write()
        {

            service.Update(c_22);

        }

    }
}
