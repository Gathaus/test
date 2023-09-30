using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace POI.Domain.Entities.ExaEntities.Models
{
    public class Company 
    {
        public Company()
        {
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string? Name { get; set; }
        public bool IsPassive { get; set; }

    }
}