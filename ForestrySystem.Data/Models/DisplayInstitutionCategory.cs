using ForestrySystem.Enums;
using ForestrySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Data.Models
{
    public class DisplayInstitutionCategory
    {
        public ForestryInstitution institution { get; set; }
        public CategoryOfTimberEnum categoryOfTimber { get; set; }
        public OriginEnum TimberName { get; set; }
        public string Year { get; set; }         
    }
}
