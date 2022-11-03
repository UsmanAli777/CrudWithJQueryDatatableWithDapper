using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudWithJQueryDatatable.App_Start.StartUp))]

namespace CrudWithJQueryDatatable.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}