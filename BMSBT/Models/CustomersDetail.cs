// Models/CustomersDetail.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMSBT.Models;

public partial class CustomersDetail
{
    [Key]
    public int Uid { get; set; }

    [Required]
    [Display(Name = "Customer No")]
    public string CustomerNo { get; set; } = null!;

    [Display(Name = "BT No")]
    public string? Btno { get; set; }

    [Display(Name = "Customer Name")]
    public string? CustomerName { get; set; }

    [Display(Name = "Generated Month/Year")]
    public string? GeneratedMonthYear { get; set; }

    [Display(Name = "Location Seq No")]
    public string? LocationSeqNo { get; set; }

    [Display(Name = "CNIC No")]
    public string? Cnicno { get; set; }

    [Display(Name = "Father Name")]
    public string? FatherName { get; set; }

    [Display(Name = "Installed On")]
    public string? InstalledOn { get; set; }

    [Display(Name = "Mobile No")]
    public string? MobileNo { get; set; }

    [Display(Name = "Telephone No")]
    public string? TelephoneNo { get; set; }

    [Display(Name = "Meter Type")]
    public string? MeterType { get; set; }

    [Display(Name = "NTN Number")]
    public string? Ntnnumber { get; set; }

    public string? City { get; set; }

    [Required]
    public string Project { get; set; } = null!;

    [Required]
    [Display(Name = "Sub Project")]
    public string SubProject { get; set; } = null!;

    [Required]
    [Display(Name = "Tariff Name")]
    public string TariffName { get; set; } = null!;

    [Display(Name = "Bank No")]
    public string? BankNo { get; set; }

    [Display(Name = "BT No Maintenance")]
    public string? BtnoMaintenance { get; set; }

    [Required]
    public string Category { get; set; } = null!;

    [Required]
    public string Block { get; set; } = null!;

    [Display(Name = "Plot Type")]
    public string? PlotType { get; set; }

    public string? Size { get; set; }

    [Required]
    public string Sector { get; set; } = null!;

    [Required]
    [Display(Name = "Plot No")]
    public string PloNo { get; set; } = null!;

    [Display(Name = "Bill Status Maintenance")]
    public string? BillStatusMaint { get; set; }

    [Display(Name = "Bill Status")]
    public string? BillStatus { get; set; }

    [Display(Name = "Bill Generation Status")]
    public string? BillGenerationStatus { get; set; }

    public string? History { get; set; }

    [Display(Name = "Meter No")]
    public string? MeterNo { get; set; }
}