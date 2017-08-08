using Deploy;
using Deploy.DAL;
using Deploy.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.Service
{
    public class DeployTypesService
    {
        private readonly DeployDBContext _context;

        public DeployTypesService(DeployDBContext context)
        {
            _context = context;
        }


        //#########################################
        //##        CreateTennantParams          ##
        //#########################################

        public DeployChoiceViewModel CreateDeployTypes (DeployChoiceViewModel choices)
        {
            //var DeployChoices = _context.DeployChoices.Where(d => d.BaseOption == choices.DeployName && d.Domain == choices.Domain && d.Datadisk == choices.Datadisk && d.Size == choices.Size).FirstOrDefault();
            var DeployChoices = _context.DeployChoices.Where(d => d.BaseOption == choices.DeployName).FirstOrDefault();

            choices.DeployFile = DeployChoices.DeployFile;
            choices.ParamsFile = DeployChoices.ParamsFile;
            choices.DeployName = DeployChoices.DeployName;
            choices.DeployCode = DeployChoices.DeployCode;

            return choices;
        }

    }
}
