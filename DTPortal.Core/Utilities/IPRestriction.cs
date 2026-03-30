using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTPortal.Core.Domain.Repositories;
using DTPortal.Core.Domain.Services.Communication;
using IPNumbers;
using Microsoft.Extensions.Logging;

namespace DTPortal.Core.Utilities
{
    public class IPRrestriction : IIpRestriction
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<IPRrestriction> _logger;

        public IPRrestriction(ILogger<IPRrestriction> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Comma seperated string of allowable IPs. Example
        // "10.2.5.41,192.168.0.22"
        public string AllowedSingleIPs { get; set; }

        // Comma seperated string of allowable IPs with masks.
        // Example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0"
        public string AllowedMaskedIPs { get; set; }

        // List of allowed IPs
        IPList allowedIPListToCheck = new IPList();

        // Comma seperated string of denied IPs. Example "10.2.5.41,192.168.0.22"
        public string DeniedSingleIPs { get; set; }

        // Comma seperated string of denied IPs with masks.
        // Example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0"
        public string DeniedMaskedIPs { get; set; }

        // List of denied IPs
        IPList deniedIPListToCheck = new IPList();

        //private async Task<bool> CheckAllowedIPs(string userIpAddress)
        //{
        //    _logger.LogDebug("-->CheckAllowedIPs");

        //    if (string.IsNullOrEmpty(userIpAddress))
        //    {
        //        _logger.LogError("Invalid Input Parameter");
        //        return false;
        //    }

        //    AllowedSingleIPs = await _unitOfWork.IpBasedAccess.
        //        GetActiveAllowedSingleIps();
        //    if(null != AllowedSingleIPs)
        //    {
        //        // Populate the IPList with the Single IPs
        //        if (!string.IsNullOrEmpty(AllowedSingleIPs))
        //        {
        //            SplitAndAddSingleIPs(AllowedSingleIPs, allowedIPListToCheck);
        //        }
        //    }

        //    AllowedMaskedIPs = await _unitOfWork.IpBasedAccess.
        //        GetActiveAllowedMaskedIps();
        //    if (null != AllowedMaskedIPs)
        //    {
        //        // Populate the IPList with the Masked IPs
        //        if (!string.IsNullOrEmpty(AllowedMaskedIPs))
        //        {
        //            SplitAndAddMaskedIPs(AllowedMaskedIPs, allowedIPListToCheck);
        //        }
        //    }

        //    _logger.LogDebug("<--CheckAllowedIPs");
        //    return allowedIPListToCheck.CheckNumber(userIpAddress);
        //}

        // Checks the denied IPs.
        //private async Task<bool> CheckDeniedIPs(string userIpAddress)
        //{
        //    _logger.LogDebug("-->CheckDeniedIPs");

        //    if (string.IsNullOrEmpty(userIpAddress))
        //    {
        //        _logger.LogError("Invalid Input Parameter");
        //        return false;
        //    }

        //    DeniedSingleIPs = await _unitOfWork.IpBasedAccess.
        //        GetActiveDeniedSingleIps();
        //    if(null != DeniedSingleIPs)
        //    {
        //        // Populate the IPList with the Single IPs
        //        if (!string.IsNullOrEmpty(DeniedSingleIPs))
        //        {
        //            SplitAndAddSingleIPs(DeniedSingleIPs, deniedIPListToCheck);
        //        }
        //    }

        //    DeniedMaskedIPs = await _unitOfWork.IpBasedAccess.
        //        GetActiveDeniedMaskedIps();
        //    if (null != DeniedMaskedIPs)
        //    {
        //        // Populate the IPList with the Masked IPs
        //        if (!string.IsNullOrEmpty(DeniedMaskedIPs))
        //        {
        //            SplitAndAddMaskedIPs(DeniedMaskedIPs, deniedIPListToCheck);
        //        }
        //    }

        //    _logger.LogDebug("<--CheckDeniedIPs");
        //    return deniedIPListToCheck.CheckNumber(userIpAddress);
        //}

        // Splits the incoming ip string of the format "IP,IP" example
        // "10.2.0.0,10.3.0.0" and adds the result to the IPList
        private void SplitAndAddSingleIPs(string ips, IPList list)
        {
            _logger.LogDebug("-->SplitAndAddSingleIPs");

            if (string.IsNullOrEmpty(ips) || null == list)
            {
                _logger.LogError("Invalid Input Parameter");
                return;
            }

            var splitSingleIPs = ips.Split(',');
            foreach (string ip in splitSingleIPs)
                list.Add(ip);

            _logger.LogDebug("<--SplitAndAddSingleIPs");
        }

        // Splits the incoming ip string of the format "IP;MASK,IP;MASK"
        // example "10.2.0.0;255.255.0.0,10.3.0.0;255.255.0.0" and adds the
        // result to the IPList
        private void SplitAndAddMaskedIPs(string ips, IPList list)
        {
            _logger.LogDebug("-->SplitAndAddMaskedIPs");

            if (string.IsNullOrEmpty(ips) || null == list)
            {
                _logger.LogError("Invalid Input Parameter");
                return;
            }

            var splitMaskedIPs = ips.Split(',');
            foreach (string maskedIp in splitMaskedIPs)
            {
                var ipAndMask = maskedIp.Split('-');
                list.AddRange(ipAndMask[0], ipAndMask[1]); // IP;MASK
            }

            _logger.LogDebug("<--SplitAndAddMaskedIPs");
        }

        //public async Task<bool> CheckIPRestriction(string ip)
        //{
        //    _logger.LogDebug("-->CheckIPRestriction");
        //    bool finallyAllowed = false;

        //    _logger.LogInformation("RECEIVED IP : {0}", ip);

        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        _logger.LogError("Invalid Input Parameter");
        //        return finallyAllowed;
        //    }

        //    try
        //    {
        //        // Check that the IP is allowed to access
        //        bool ipAllowed = await CheckAllowedIPs(ip);
        //        if(!ipAllowed)
        //        {
        //            _logger.LogWarning("{0}, IP is not in Allowed List", ip);
        //            return false;
        //        }

        //        _logger.LogInformation("IP is in Allowed List");

        //        // Check that the IP is not denied to access
        //        bool ipDenied = await CheckDeniedIPs(ip);
        //        if (ipDenied)
        //        {
        //            _logger.LogWarning("{0} IP is in Denied List", ip);
        //            return false;
        //        }

        //        _logger.LogInformation("{0} IP is not in Denied List", ip);

        //        // Only allowed if allowed and not denied
        //        finallyAllowed = true;

        //        _logger.LogInformation("IP HAS ACCESS");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("CheckIPRestriction failed: {0}", ex.Message);
        //        return false;
        //    }

        //    _logger.LogDebug("<--CheckIPRestriction");
        //    return finallyAllowed;
        //}
    }
}