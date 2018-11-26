using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }
        public string Action
        {
            get
            {
                //var update = (c => c.Update());//c => c.Update() is an anonymous method so we ca use func delegate to represent this
                //Func is a delagate which allows us to call the anonymous method but we dont want to call it jsut represent it so we changed type from func to expression func
                Expression <Func<GigsController, ActionResult>> update = 
                    (g => g.Update(this));

                Expression <Func<GigsController, ActionResult>> create = 
                    (g => g.Create(this));

                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;

            }
        }

        //renamed DateTime to getDataTime using F2 resharper then turned GetDateTime to a method
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}