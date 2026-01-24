using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Application.TrainingClasses.DTOs
{
    public class CreateTrainingClassResponse
    {
        public Guid Id { get; set; }
        public string Message {get;set;} = string.Empty;
    }
}