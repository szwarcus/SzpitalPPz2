using FluentDateTime;
using Hospital.Model.Entities;
using Hospital.Service.OutDTOs;
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
        public int startHour { get; set; }
        public int stopHour { get; set; }
        public ICollection<CurrentVisitOutDTO> Events { get; set; }
        public DateTime StartTime { get; set; }

        private List<string> headers = new List<string>() { "Godziny", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
        private DateTime StopTime;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StopTime = StartTime.SetHour(stopHour);

            output.TagName = "section";
            output.Attributes.Add("class", "calendar");
            output.Content.SetHtmlContent(GetHtml());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetHref(CurrentVisitOutDTO visit)
        {
            if (visit.State == Core.Enums.StateVisit.Created)
                return $"/{Area}/{Controller}/{Action}?VisitId={visit.Id}";
            else
                return "";
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
        private string getHrefWeek(DateTime dateTime)
        {
            return $"/{Area}/{Controller}/TimeTable?dateTime={dateTime.ToLongDateString()}";
        }

        private string GetHtml()
        {
            var html = new XDocument(
                new XElement("div",
                    new XAttribute("class", "container-fluid "),
                    new XElement("header",
                     
                       new XElement("div", new XAttribute("id", "selectDateWrapper"),
                             new XAttribute("class", " d-flex justify-content-center"),
                             new XElement("img", new XAttribute("class", " d-inline-block NextPreviousWeek"), new XAttribute("href", $"{getHrefWeek(StartTime.AddDays(-7))}"), new XAttribute("src", "/images/left-arrow.svg")),
                             new XElement("h5",
                                new XAttribute("class", "d-inline-block  text-center my-auto"),
                                $"Wyświetlany tydzień: {StartTime.ToString("dd/MM/yyyy")} - {StartTime.Next(DayOfWeek.Monday).ToString("dd/MM/yyyy")}"
                                ),
                             new XElement("input", new XAttribute("id","datePicker"), new XAttribute("name", "datePicker"), new XAttribute("type", "hidden")),
                             new XElement("img", new XAttribute("class", " d-inline-block  NextPreviousWeek"), new XAttribute("href", $"{getHrefWeek(StartTime.AddDays(7))}"), new XAttribute("src", "/images/right-arrow.svg"))
                            )),
                        new XElement("div",
                            new XAttribute("class", "row d-none d-lg-flex p-1 bg-dark text-white"),
                            headers.Select(d =>
                                new XElement("h6",
                                    new XAttribute("class", "col-lg p-1 text-center"),
                                    d.ToString()
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
                var numberAppappointment = ((StopTime.Hour - StartTime.Hour)) * 2 * 8;

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
                                     new XAttribute("class", $"col-lg p-0 border border-left-0 border-top-0 text-truncate  bg-dark text-white text-center  "),
                                          new XElement("h6",
                                          new XAttribute("class", "text-center m-0"),
                                           $"{GetHourWork()}"));
                    }
                    else
                    {

                        var mutedClasses = "d-none d-lg-inline-block bg-light text-muted";
                        var currentClass = ".bg-success";
                        yield return new XElement("div",
                                     new XAttribute("class", $"{((DateTime.Now >= StartTime && DateTime.Now < StartTime.AddMinutes(30)) ? currentClass : null)} col-lg p-0 border border-left-0 border-top-0 text-truncate {((i % 8 == 6 || i % 8 == 7) ? mutedClasses : null)}"),
                                         GetEventHtml(StartTime.AddMinutes(-30))

                                     );
                        StartTime = StartTime.AddDays(1);
                    }
                }
            }

            XElement GetEventHtml(DateTime d)
            {
                CurrentVisitOutDTO existEvent = null;
                if (Events != null)
                    existEvent = Events.FirstOrDefault(el => el.DateTime >= d && el.DateTime < d.AddMinutes(30));
                if (existEvent == null)
                {
                    return new XElement("h6",
                                    new XAttribute("class", "date text-center"),
                                    ""
                                       );
                }
                else
                {
                    return
                           new XElement("div",
                                    // new XAttribute("class", $"btn {((DateTime.Now >= d && DateTime.Now < d.AddMinutes(30)) ? "btn-success" : "btn-secondary")} b btn-lg active d-flex "),
                                    new XAttribute("class", $"{((existEvent.State == Core.Enums.StateVisit.Completed) ? "completedVisits" : "upComingVisits")}"),
                                    //new XAttribute("role", "button"),
                                    new XAttribute("href", $"{GetHref(existEvent)}"),
                                    new XElement("h6",
                                             new XAttribute("class", "date text-center m-0"),
                                             "Wizyta"
                                       )


                        );

                }
            }
        }
    }
}
