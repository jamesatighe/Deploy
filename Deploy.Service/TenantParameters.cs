using System;
using Deploy.DAL;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Deploy.ViewModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;
using Deploy.Service;

namespace Deploy.Service
{
    public class TenantParameters
    {
        private readonly DeployDBContext _context;
        private readonly AzureStorageConfig _storageConfig;

        public TenantParameters(DeployDBContext context, AzureStorageConfig config)
        {
            _context = context;
            _storageConfig = config;
        }

//#########################################
//##        GetTennantParams             ##
//#########################################

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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID

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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName.Contains("(IDMTYPE)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADMTYPE").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "Identity Medium Extended")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADMEXT").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "Identity Small Extended")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "ADSEXT").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "RDS Small (No FS)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSSNOFS").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }


                if (viewModel.DeployName.Contains("(RDSMTYPE)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "RDSMTYPE").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }


                if (viewModel.DeployName == "FS Medium (TypeTec)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "FSMTYPE").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName.Contains("VPN"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VPN").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }


                if (viewModel.DeployName.Contains("File Server (2 node)"))
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "FILESRVMED").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "VM (Domain, Data Disk)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VMDOMDISK").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "VM (Domain, No Data Disk)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VMDOMNODISK").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }

                if (viewModel.DeployName == "VM (No Domain, Data Disk)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VMNODOMDISK").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }


                if (viewModel.DeployName == "VM (No Domain, No Data Disk)")
                {
                    viewModel.DeployTypeID = TennantParams.DeployTypeID;
                    viewModel.TennantName = DeployTypes.Tennants.TennantName;
                    viewModel.TennantID = DeployTypes.Tennants.TennantID;

                    var Params = await _context.DeployParms.Where(d => d.ParameterDeployType == "VMNODOMNODISK").ToListAsync();
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
                            ParamValue = tennant.ParamValue,
                            ParamType = tennant.ParamType,
                            ParamName = tennant.ParamName,
                            ParamToolTip = tennant.ParamToolTip,
                            TennantParamID = tennant.TennantParamID
                        });
                    }

                }


                return viewModel;

            }
            throw new NotImplementedException();
        }

//#########################################
//##        CreateTennantParams          ##
//#########################################

        public async Task<TennantDeployParamsViewModel> CreateTenantParams(int Id)
        {
            var deploy = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var viewModel = new TennantDeployParamsViewModel();

            viewModel.DeployTypeID = Id;
            viewModel.DeployName = deploy.DeployName;
            viewModel.DeploySaved = deploy.DeploySaved;

            var parameters = await _context.DeployParms.Where(d => d.ParameterDeployType == deploy.DeployCode).ToListAsync();

            viewModel.DeployParams = new List<DeployParam>();
            viewModel.TennantName = deploy.Tennants.TennantName;
            viewModel.TennantID = deploy.Tennants.TennantID;

            foreach (var param in parameters)
            {
                viewModel.DeployParams.Add(new DeployParam()
                {
                    DeployParamID = param.DeployParamID,
                    ParameterName = param.ParameterName,
                    ParameterType = param.ParameterType,
                    ParameterDeployType = param.ParameterDeployType,
                    ParamToolTip = param.ParamToolTip
                });
            }

            return viewModel;
        }

//#########################################
//##        DeployToAzure                ##
//#########################################

        public async Task<string[]> DeployToAzure(int Id, bool Force)
        {

            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            string[] resultsarr = new string[3];
            //Declare variables for use
            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.ResourceGroupName;
            if (string.IsNullOrEmpty(resourcegroupname) == true)
            {
                resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            }
            string resourcegrouplocation = deployTypes.Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;


            string jsonResourceGroup = "{ \"location\": \"{resourcegrouplocation}\" }";
            jsonResourceGroup = jsonResourceGroup.Replace("{resourcegrouplocation}", resourcegrouplocation);

            //Get Access Token from HTTP POST to Azure
            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            //Create json for deployment to be amended.
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            //Add new Deployment Type logic here!

            jsonDeploy = jsonDeploy.Replace("{template}", deployTypes.DeployFile);
            var DepParams = deployTypes.ParamsFile;
            DepParams = DepParams.Replace("{tennant}", deployTypes.Tennants.TennantName);
            DepParams = DepParams.Replace("{id}", deployTypes.DeployTypeID.ToString());
            jsonDeploy = jsonDeploy.Replace("{parameters}", DepParams);
            azuredeploy = deployTypes.AzureDeployName;

           
            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonResourceGroup, true);

            //PUT request for deployment.
            var ValidateContent = RESTApi.Validate(subscriptionID, resourcegroupname, azuredeploy, accesstoken).Result;
            if (ValidateContent == "DeployExists" && Force == false)
            {
                resultsarr[0] = "DeployExists";
                resultsarr[1] = "";
                resultsarr[2] = "";
                return resultsarr;
            }
            else
            {
                var ValidateTemplate = RESTApi.ValidateTemplate(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy).Result;
                if (ValidateTemplate[0] == "TemplateValid")
                {

                    var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
                    JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

                    //Update Deployment Type to show deployed
                    deployTypes.DeployState = "Deployed";
                    deployTypes.DeployResult = await putcontent;
                    _context.Update(deployTypes);
                    await _context.SaveChangesAsync();
                    ValidateContent = "False";
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = "TemplateValid";
                    resultsarr[2] = "";
                    return resultsarr;
                }
                else
                {
                    resultsarr[0] = "DeployNotExists";
                    resultsarr[1] = ValidateTemplate[0];
                    resultsarr[2] = ValidateTemplate[1];
                    return resultsarr;
                }
            }
        }



//#########################################
//##        DeployRDSSmall               ##
//#########################################

        public async Task DeployRDSSmall(int Id)
        { 
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName.Contains("rdssmallsolution")).ToListAsync();

            //Declare variables for use
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes[0].Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            string jsonResourceGroup = "{ \"location\": \"North Europe\" }";

            //string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/Parameters/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\" } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            jsonDeploy = jsonDeploy.Replace("{template}", "Linked/rdssmallsolution-temp.json");
            jsonDeploy = jsonDeploy.Replace("{parameters}", "identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json");
            azuredeploy = "rdssmallsolution";

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/Linked");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("rdssmallsolution.json");

            string linkedTemplate;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                linkedTemplate = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Network/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/VNET-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identitysmall.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSSmallfull.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdssmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);

            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdssmallsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);


            var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Update Deployment Type to show deployed
            foreach (var deploy in deployTypes)
            {
                deploy.DeployState = "Deployed";
                deploy.DeployResult = await putcontent;
                _context.Update(deploy);
                await _context.SaveChangesAsync();
            }



        }

//#########################################
//##        DeployRDSMed                 ##
//#########################################

        public async Task DeployRDSMed(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.AzureDeployName.Contains("rdsmedsolution")).ToListAsync();

            //Declare variables for use
            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string resourcegrouplocation = deployTypes.FirstOrDefault().Tennants.ResourceGroupLocation;
            string azuredeploy = string.Empty;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var sasToken = AzureHelper.GetSASToken(_storageConfig);


            string jsonResourceGroup = "{ \"location\": \"{resourcegrouplocation}\" }";
            jsonResourceGroup = jsonResourceGroup.Replace("{resourcegrouplocation}", resourcegrouplocation);

            //string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\", \"parametersLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/Parameters/{parameters}{sasToken}\", \"contentVersion\": \"1.0.0.0\" } } }";
            string jsonDeploy = "{\"properties\": { \"templateLink\": { \"uri\": \"https://cobwebjson.blob.core.windows.net/ansible/{template}{sasToken}\", \"contentVersion\": \"1.0.0.0\"}, \"mode\": \"Incremental\" } }";
            jsonDeploy = jsonDeploy.Replace("{sasToken}", sasToken);

            //Set Deploy Template dependent on Deployment Type
            jsonDeploy = jsonDeploy.Replace("{template}", "Linked/rdsmedsolution-temp.json");
            //jsonDeploy = jsonDeploy.Replace("{parameters}", "identitysmall-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json");

            Guid guid = Guid.NewGuid();
            azuredeploy = "rdsmedsolution" + guid;

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/Linked");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("rdsmedsolution.json");

            string linkedTemplate;
            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                linkedTemplate = Encoding.UTF8.GetString(memoryStream.ToArray());
            }



            linkedTemplate = linkedTemplate.Replace("{templatelinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Network/VNet1SubnetsGW.json" + sasToken);
            linkedTemplate = linkedTemplate.Replace("{parameterlinkvnet}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/VNET-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            if (deployTypes.FirstOrDefault().Tennants.TennantName.Contains("TypeTec"))
            {
                linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identityMDType.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/identity-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSMediumfullType.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmedium-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            }
            else
            {
                linkedTemplate = linkedTemplate.Replace("{templatelinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Identity/identityMD.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkid}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/identity-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{templatelinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/RDS/RDSMediumfull.json" + sasToken);
                linkedTemplate = linkedTemplate.Replace("{parameterlinkrds}", "https://cobwebjson.blob.core.windows.net/ansible/Parameters/rdsmedium-" + deployTypes.FirstOrDefault().Tennants.TennantName + "-param.json" + sasToken);
            }
            
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("rdsmedsolution-temp.json");
            using (Stream s = GenerateStreamFromString(linkedTemplate))
            {
                await blockBlob2.UploadFromStreamAsync(s);
            }

            var putResourceGroup = RESTApi.PutAsync(subscriptionID, resourcegroup, azuredeploy, accesstoken, jsonResourceGroup, true);


            var putcontent = RESTApi.PutAsync(subscriptionID, resourcegroupname, azuredeploy, accesstoken, jsonDeploy, false);
            JObject json = JsonConvert.DeserializeObject<JObject>(putcontent.Result);

            //Update Deployment Type to show deployed
            foreach (var deploy in deployTypes)
            {
                deploy.DeployState = "Deployed";
                deploy.DeployResult = await putcontent;
                _context.Update(deploy);
                await _context.SaveChangesAsync();
            }
        }



 //#########################################
//##        GetDeploy                    ##
//#########################################

        public async Task GetDeploy(int Id)
        {
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();

            string tennantID = deployTypes.Tennants.AzureTennantID;
            string clientID = deployTypes.Tennants.AzureClientID;
            string secret = deployTypes.Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.Tennants.AzureSubscriptionID;
            string resourcegroupname = deployTypes.ResourceGroupName;
            if (string.IsNullOrEmpty(resourcegroupname) == true)
            {
                resourcegroupname = deployTypes.Tennants.ResourceGroupName;
            }
            string resourcegroup = deployTypes.Tennants.ResourceGroupName;
            string azuredeploy = deployTypes.AzureDeployName;

            var results = RESTApi.PostAction(tennantID, clientID, secret);
            RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
            string accesstoken = AccessToken.access_token;

            var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname, accesstoken, azuredeploy);

            if (await getcontent == null)
            {
                deployTypes.DeployResult = "";
            }
            else
            {
                deployTypes.DeployResult = await getcontent;
            }
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
        }

//#########################################
//##        GetDeployAll                 ##
//#########################################

        public async Task GetDeployAll(int Id)
        {
            var deployTypes = await _context.DeployTypes.Include(d => d.Tennants).Where(d => d.TennantID == Id).Where(d => d.DeployState != null).ToListAsync();

            string tennantID = deployTypes.FirstOrDefault().Tennants.AzureTennantID;
            string clientID = deployTypes.FirstOrDefault().Tennants.AzureClientID;
            string secret = deployTypes.FirstOrDefault().Tennants.AzureClientSecret;
            string subscriptionID = deployTypes.FirstOrDefault().Tennants.AzureSubscriptionID;
            //string resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            //if (string.IsNullOrEmpty(resourcegroupname) == true)
            //{
            //    resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            //}
            string resourcegroup = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
            string azuredeploy = string.Empty;

            //Add logic for getting deployment for solutions.
            //Example for rdssmallsolution below.

            for (var i = 0; i < deployTypes.Count(); i++)
            {
                string resourcegroupname = deployTypes[i].ResourceGroupName;
                if (string.IsNullOrEmpty(resourcegroupname) == true)
                {
                    resourcegroupname = deployTypes.FirstOrDefault().Tennants.ResourceGroupName;
                }
                if (deployTypes[i].AzureDeployName.Contains("rdssmallsolution"))
                {
                    if (deployTypes[i].DeployName.Contains("(IDS)") || deployTypes[i].DeployName.Contains("Identity Small"))
                    {
                        azuredeploy = "IDSmall";
                    }
                    if (deployTypes[i].DeployName.Contains("(RDSS)") || deployTypes[i].DeployName.Contains("RDS Small"))
                    {
                        azuredeploy = "RDSSmall";
                    }
                    if (deployTypes[i].DeployName.Contains("(VNET)"))
                    {
                        azuredeploy = "VNET";
                    }
                }
                else
                {
                    if (deployTypes[i].AzureDeployName.Contains("rdsmedsolution"))
                    {
                        if (deployTypes[i].DeployName.Contains("(IDM)") || deployTypes[i].DeployName.Contains("Identity Medium") || deployTypes[i].DeployName.Contains("(IDMTYPE)"))
                        {
                            azuredeploy = "IDMed";
                        }
                        if (deployTypes[i].DeployName.Contains("(RDSM)") || deployTypes[i].DeployName.Contains("RDS Medium") || deployTypes[i].DeployName.Contains("(RDSMTYPE)"))
                        {
                            azuredeploy = "RDSMed";
                        }
                        if (deployTypes[i].DeployName.Contains("(VNET)"))
                        {
                            azuredeploy = "VNET";
                        }
                    }

                    else
                    {
                      azuredeploy = deployTypes[i].AzureDeployName;
                    }
                }
                var results = RESTApi.PostAction(tennantID, clientID, secret);
                RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
                string accesstoken = AccessToken.access_token;

                var getcontent = RESTApi.GetAsync(subscriptionID, resourcegroupname, accesstoken, azuredeploy);

                if (await getcontent == null)
                {
                    deployTypes[i].DeployResult = " ";
                }
                else
                {
                    deployTypes[i].DeployResult = await getcontent;
                }
                _context.Update(deployTypes[i]);
                await _context.SaveChangesAsync();
            }
            //End of solution logic loop.
        }

//#########################################
//##        SaveToAzure                  ##
//#########################################

        public async Task SaveToAzure(int Id)
        {
            var tennantParams = await _context.TennantParams.Where(t => t.DeployTypeID == Id).ToListAsync();
            var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
            var sasToken = AzureHelper.GetSASToken(_storageConfig);

            var encrypt = new RESTApi(_storageConfig);
            string[] Keys = await encrypt.EncryptionKeys();
            var encryption = new Encryption(Keys[1], Keys[0], 1, Keys[2], 256);

            string filename = deployTypes.ParamsFile;
            filename = filename.Replace("{tennant}", deployTypes.Tennants.TennantName);
            filename = filename.Replace("{id}", deployTypes.DeployTypeID.ToString());


            //string filename = "{deploytype}-" + deployTypes.Tennants.TennantName + "-" + deployTypes.DeployTypeID + "-param.json";
            string solutionfilename = "{deploytype}-" + deployTypes.Tennants.TennantName + "-param.json";

            /*Solution logic */
            if (deployTypes.DeployName.Contains("(VNET)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "VNET");
            }
            if (deployTypes.DeployName.Contains("(IDS)")) 
            {
                filename = solutionfilename.Replace("{deploytype}", "identitysmall");
            };
            if (deployTypes.DeployName.Contains("(IDM)")) 
            {
                filename = solutionfilename.Replace("{deploytype}", "identity");
            };
            if (deployTypes.DeployName.Contains("(IDMTYPE)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "identity");
            };
            if (deployTypes.DeployName.Contains("(RDSS)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "rdssmall");
            };
            if (deployTypes.DeployName.Contains("(RDSM)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "rdsmedium");
            };
            if (deployTypes.DeployName.Contains("RDSMTYPE)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "rdsmedium");
            };
            if (deployTypes.DeployName.Contains("(RDSSTYPE)"))
            {
                filename = solutionfilename.Replace("{deploytype}", "rdssmall");
            };

            var JsonHeader = new Dictionary<string, string>();
            JsonHeader.Add("$schema", "https:\\schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#");
            JsonHeader.Add("contentVersion", "1.0.0.0");

            //Create String Builder to hold generated Json parameters file.
            StringBuilder tempjson = new StringBuilder();
            tempjson.AppendLine("{");
            tempjson.AppendLine("\t\"$schema\": \"https://schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#\",");
            tempjson.AppendLine("\t\"contentVersion\": \"1.0.0.0\",");
            tempjson.AppendLine("\t\"parameters\": {");

            //Loop through each parameter for the deployment and add them under the parameters Json key.
            for (var i = 0; i < tennantParams.Count(); i++)
            {
                tempjson.AppendLine("\t\t\"" + tennantParams[i].ParamName.ToString() + "\": {");
                if (tennantParams[i].ParamType == "domainadmin" || tennantParams[i].ParamType == "directorypath")
                {

                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString().Replace(@"\", @"\\") + "\"");
                }
                else if (tennantParams[i].ParamType == "array")
                {
                    tempjson.AppendLine("\t\t\t\"value\": [");
                    tempjson.AppendLine("\t\t\t\t" + tennantParams[i].ParamValue.ToString());
                    tempjson.AppendLine("\t\t\t]");
                }
                else if (tennantParams[i].ParamType == "password")
                {
                    var decrypted = encryption.DecryptString(tennantParams[i].ParamValue);
                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + decrypted + "\"");
                }
                else
                {
                    tempjson.AppendLine("\t\t\t\"value\": " + "\"" + tennantParams[i].ParamValue.ToString() + "\"");
                }
                if (i < tennantParams.Count - 1)
                {
                    tempjson.AppendLine("\t\t},");
                }

                if (i == tennantParams.Count - 1)
                {
                    tempjson.AppendLine("\t\t}");
                }

            };

            tempjson.AppendLine("\t}");
            tempjson.AppendLine("}");

            var jsonfull = tempjson.ToString();

            CloudStorageAccount storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ansible/");
            CloudBlockBlob blockBlob1 = container.GetBlockBlobReference(filename);

            using (Stream s = GenerateStreamFromString(jsonfull))
            {
                await blockBlob1.UploadFromStreamAsync(s);
            }

            deployTypes.DeploySaved = "Yes";
            _context.Update(deployTypes);
            await _context.SaveChangesAsync();
        }

//#########################################
//##        GenerateSteamFromString      ##
//#########################################

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}
