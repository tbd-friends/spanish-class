using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Microsoft.AspNetCore.WebUtilities;

namespace site.Pages
{
    public class LandingPage : ComponentBase
    {
        protected StudentModel Model { get; set; }
        
        [Parameter]
        public string EntryCode { get; set; }

        [Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected ProtectedSessionStorage ProtectedSessionStore { get; set; }

        public LandingPage()
        {
            Model = new StudentModel();
        }

        protected void HandleValidSubmit()
        {
            ProtectedSessionStore.SetAsync("name", Model.Name);
            ProtectedSessionStore.SetAsync("start", DateTime.Now);
            ProtectedSessionStore.SetAsync("entry", EntryCode);

            Navigation.NavigateTo("/puzzle1");
        }
    }

    public class StudentModel
    {
        public string Name { get; set; }
    }
}
