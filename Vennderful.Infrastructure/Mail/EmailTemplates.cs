using System.Text;

namespace Vennderful.Infrastructure.Mail
{
    public static class EmailTemplates
    {
        #region Invitation
        public static string Invitation_Subject = "Invitation to Vennderful";

        public static string Invitation_Body(string company, string role)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<h2>Hello!</h2>");
            sb.Append($"<h3>You are invited to signup to Vennderful as {role}</h3>");
            sb.Append("<p>Please accept by <a href=\"https://qa-vennder.azurewebsites.net/signup\" class=\"btn btn-primary\">SINGUP NOW!</a></p>");

            return sb.ToString();
        }
        #endregion

        #region EventInvitation
        public static string EventInvitation_Subject = "Event Invitation";

        public static string EventInvitation_Body(string name, string evnt, Guid eventId, Guid clientId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<h2>Hello {name}</h2>");
            sb.Append($"<h3>You are invited to {evnt} event</h3>");
            sb.Append($"<p>Please <a href=\"https://qa-vennder.azurewebsites.net/signup/{eventId}/{clientId} \" class=\"btn btn-primary\">Accept Invitation</a></p>");

            return sb.ToString();
        }
        #endregion
    }
}
