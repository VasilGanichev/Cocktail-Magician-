using CocktailMagician.Data.Entities;
using CocktailMagicianWeb.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagicianWeb.Models
{
    public class EventViewModel
    {
        public EventViewModel()
        {
            EventTags = Enum.GetNames(typeof(EventTags));
        }

        public EventViewModel(Bar bar)
        {
            EventTags = Enum.GetNames(typeof(EventTags));
            BarId = bar.Id;
            BarName = bar.Name;
            Address = bar.Address;
            BarPhone = bar.PhoneNumber;

        }

        public EventViewModel(EventViewModel model)
        {
            EventTags = Enum.GetNames(typeof(EventTags));
            BarId = model.Id;
            BarName = model.BarName;
            Address = model.Address;
            BarPhone = model.BarPhone;
            BarEmail = model.BarEmail;
            BarMenuHtmlString = model.BarMenuHtmlString;
        }
        public int Id { get; set; }

        public int BarId { get; set; }

        public string Host { get; set; }

        public string BarName { get; set; }

        public string Address { get; set; }

        public bool IsPrivate { get; set; }

        public bool SendMenu { get; set; }

        public string[] EventTags { get; set; }

        public DateTime EventStart { get; set; }

        public string Description { get; set; }

        public string BarPhone { get; set; }

        public string BarEmail { get; set; }

        public string InvitationEmailAddresses { get; set; }

        public string SelectedTags { get; set; }

        public List<string> AttachmentsPaths { get; set; }

        public string BarViewHtmlString { get; set; }

        public string BarMenuHtmlString { get; set; }

        public override string ToString()
        {
            var eventAcces = IsPrivate == true ? "private" : "public";
            var sb = new StringBuilder();
            sb.AppendLine("Greetings!");
            sb.AppendLine($"We are very pleased to inform you that {Host} have invited you to his/her {eventAcces} event!");
            sb.AppendLine($"It will take place at '{BarName}' exactly at {EventStart}");
            sb.AppendLine($"Please check the event details below");
            sb.AppendLine();
            sb.AppendLine($"{Description}");
            sb.AppendLine($"Event tags: {SelectedTags}");
            sb.AppendLine();
            sb.AppendLine($"For more information contact {Host}");
            sb.AppendLine($"or get in touch with {BarName} at {BarPhone} or {BarEmail}");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Kind regards,");
            sb.AppendLine("CocktailMagician Team!");
            return sb.ToString();
        }
    }
}
