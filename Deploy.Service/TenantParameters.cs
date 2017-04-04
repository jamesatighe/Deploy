using System;
using Deploy.DAL;
using Deploy;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Deploy.ViewModel;
using System.Threading.Tasks;

namespace Deploy.Service
{
    public class TenantParameters
    {
        private readonly DeployDBContext _context;
        public TenantParameters(DeployDBContext context)
        {
            _context = context;
        }

        public async Task<TennantDeployParamsViewModel> GetTenantParams(int Id)
        {

            var TennantParams = _context.TennantParams.Where(t => t.DeployTypeID == Id).FirstOrDefault();
            var DeployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            if (TennantParams != null)
            {

                var viewModel = new TennantDeployParamsViewModel();
                viewModel.DeployName = DeployTypes.DeployName;
                viewModel.DeploySaved = DeployTypes.DeploySaved;

                if (viewModel.DeployName == "Identity Small" || viewModel.DeployName.Contains("(IDS)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterDeployType = Param.ParameterDeployType
                        });
                    }
                    viewModel.TennantParams = new List<TennantParam>();
                    foreach (var tennant in tennantParams)
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {
                            ParamValue = tennant.ParamValue
                        });
                    }

                }

                if (viewModel.DeployName == "Identity Medium" || viewModel.DeployName.Contains("(IDM)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADM").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterDeployType = Param.ParameterDeployType
                        });
                    }
                    viewModel.TennantParams = new List<TennantParam>();
                    foreach (var tennant in tennantParams)
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {
                            ParamValue = tennant.ParamValue
                        });
                    }

                }

                if (viewModel.DeployName == "RDS Small" || viewModel.DeployName.Contains("(RDSS)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterDeployType = Param.ParameterDeployType
                         });
                    }
                    viewModel.TennantParams = new List<TennantParam>();
                    foreach (var tennant in tennantParams)
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {
                            ParamValue = tennant.ParamValue
                        });
                    }

                }

                if (viewModel.DeployName == "RDS Medium" || viewModel.DeployName.Contains("(RDSM)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSM").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterDeployType = Param.ParameterDeployType
                        });
                    }
                    viewModel.TennantParams = new List<TennantParam>();
                    foreach (var tennant in tennantParams)
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {
                            ParamValue = tennant.ParamValue
                        });
                    }

                }

                if (viewModel.DeployName.Contains("VNET"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VNET").ToListAsync();
                    var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
                    viewModel.DeployParams = new List<DeployParam>();
                    foreach (var Param in Params)
                    {
                        viewModel.DeployParams.Add(new DeployParam()
                        {
                            DeployParamID = Param.DeployParamID,
                            ParameterName = Param.ParameterName,
                            ParameterDeployType = Param.ParameterDeployType
                        });
                    }
                    viewModel.TennantParams = new List<TennantParam>();
                    foreach (var tennant in tennantParams)
                    {
                        viewModel.TennantParams.Add(new TennantParam()
                        {
                            ParamValue = tennant.ParamValue
                        });
                    }

                }


                return viewModel;

            }
            throw new NotImplementedException();
        }

        public async Task<TennantDeployParamsViewModel> CreateTenantParams(int Id)
        {
            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TennantDeployParamsViewModel();

            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.DeploySaved = deploy.DeploySaved;

            if (viewModel.DeployName.Contains("(IDS)") || viewModel.DeployName == "Identity Small")
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADS").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;

                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType,
                        ParamToolTip = param.ParamToolTip
                    });
                }
            }

            if (viewModel.DeployName.Contains("(IDM)") || viewModel.DeployName == "Identity Medium")
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADM").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;

                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType,
                        ParamToolTip = param.ParamToolTip
                    });
                }
            }

            if (viewModel.DeployName.Contains("(RDSS)") || viewModel.DeployName == "RDS Small")
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSS").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;
                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType,
                        ParamToolTip = param.ParamToolTip
                    });
                }
            }

            if (viewModel.DeployName.Contains("(RDSM)") || viewModel.DeployName == "RDS Medium")
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSM").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;
                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType,
                        ParamToolTip = param.ParamToolTip
                    });
                }
            }


            if (viewModel.DeployName.Contains("VNET"))
            {
                var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == "VNET").ToListAsync();
                viewModel.DeployParams = new List<DeployParam>();
                viewModel.TennantName = deploy.Tennants.TennantName;
                viewModel.TennantID = deploy.Tennants.TennantID;
                //viewModel.DeployParamID = new List<int>();

                foreach (var param in parameters)
                {
                    //viewModel.DeployParamID.Add(param.DeployParamID);

                    viewModel.DeployParams.Add(new DeployParam()
                    {
                        DeployParamID = param.DeployParamID,
                        ParameterName = param.ParameterName,
                        ParameterType = param.ParameterType,
                        ParameterDeployType = param.ParameterDeployType,
                        ParamToolTip = param.ParamToolTip
                    });
                }
            }


            return viewModel;
        }
    }
}
