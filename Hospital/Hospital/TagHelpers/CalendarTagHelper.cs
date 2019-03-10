using FluentDateTime;
using Hospital.Model.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital.TagHelpers
{
    [HtmlTargetElement("Calendar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CalendarTagHelper : TagHelper
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int EventTimeInMinutes { get; set; }
        public int startHour {get;set;}
        public int stopHour {get;set;}
        public ICollection<Visit> Events { get; set; }

        private List<string> headers = new List<string>() { "Godziny", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
        private DateTime StartTime;
        private DateTime StopTime;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Events != null)
            {
                foreach (var item in Events)
                {
                    item.Date = item.Date.AddSeconds(1);
                }
            }

            StartTime = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Now.SetHour(startHour).SetMinute(0).SetSecond(0) :  DateTime.Now.Previous(DayOfWeek.Monday).SetHour(startHour).SetMinute(0).SetSecond(0);
            StopTime = DateTime.Now.SetHour(stopHour);
            output.TagName = "section";
            output.Attributes.Add("class", "calendar");
            output.Content.SetHtmlContent(GetHtml());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetHref()
        {
            return $"/{Area}/{Controller}/{Action}";
        }

        private string GetHourWork()
        {
            string from = "";
            string to = "";

            from = ($"{StartTime.Hour}:{StartTime.Minute}");
            from = from[from.Length - 2] == ':' ? from + "0" : from;
            StartTime = StartTime.AddMinutes(EventTimeInMinutes);
            to = ($"{StartTime.Hour}:{StartTime.Minute}");
            to = to[to.Length - 2] == ':' ? to + "0" : to;

            return from + " - " + to;
        }
       
        private string GetHtml()
        {
            var html = new XDocument(
                new XElement("div",
                    new XAttribute("class", "container-fluid "),
                    new XElement("header",
                        new XElement("h4",
                            new XAttribute("class", "display-4 mb-2 text-center"),
                            $"Wyświetlany tydzień: {StartTime.ToString("dd/MM/yyyy")} - {StartTime.Next(DayOfWeek.Monday).ToString("dd/MM/yyyy")}" 
                        ),
                        new XElement("div",
                            new XAttribute("class", "row d-none d-lg-flex p-1 bg-dark text-white"),
                            headers.Select(d =>
                                new XElement("h5",
                                    new XAttribute("class", "col-lg p-1 text-center"),
                                    d.ToString()
                                )
                            )
                        )
                    ),
                    new XElement("div",
                        new XAttribute("class", "row border border-right-0 border-bottom-0"),
                        GetDatesHtml()
                    )
                )
            );

            return html.ToString();

            IEnumerable<XElement> GetDatesHtml()
            {
                var numberAppappointment = ((StopTime.Hour - StartTime.Hour) )  * 2 * 8;           

                for (int i = 0; i < numberAppappointment; i++)
                {
                    if (i % 8 == 0 && i != 0)
                    {
                        StartTime = StartTime.Previous(DayOfWeek.Monday);
                        yield return new XElement("div",
                            new XAttribute("class", "w-100"),
                            ""
                        );
                    }
                    if (i % 8 == 0)
                    {
                        yield return new XElement("div",
                                     new XAttribute("class", $"col-lg p-2 border border-left-0 border-top-0 text-truncate  bg-dark text-white text-center "),
                                          new XElement("h5",
                                          new XAttribute("class", "text-center"),
                                           $"{GetHourWork()}"));
                    }
                    else
                    {

                        var mutedClasses = "d-none d-lg-inline-block bg-light text-muted";
                        var currentClass = ".bg-success";
                        yield return new XElement("div",
                                     new XAttribute("class", $"{((DateTime.Now >= StartTime && DateTime.Now < StartTime.AddMinutes(30)) ? currentClass : null)} col-lg p-2 border border-left-0 border-top-0 text-truncate {((i % 8 == 6 || i % 8 == 7) ? mutedClasses : null)}"),
                                         GetEventHtml(StartTime.AddMinutes(-30))
                            
                                     );
                        StartTime =  StartTime.AddDays(1);
                    }
                }
            }

            XElement GetEventHtml(DateTime d)
            {
                Visit existEvent = null;
                if (Events != null)
                     existEvent = Events.FirstOrDefault(el => el.Date >= d && el.Date<d.AddMinutes(30));
                if (existEvent == null)
                {                
                    return new XElement("h6",
                                    new XAttribute("class", "date text-center"),
                                    "---"
                                       );
                }
                else
                {
                    return
                           new XElement("a",
                                    new XAttribute("class", $"btn {((DateTime.Now >= d && DateTime.Now < d.AddMinutes(30)) ? "btn-success" : "btn-secondary")} b btn-lg active d-flex "),
                                    new XAttribute("role", "button"),
                                    new XAttribute("href", "#"),
                                    "Wizyta"
                        );
                }
            }
        }
    }
}
