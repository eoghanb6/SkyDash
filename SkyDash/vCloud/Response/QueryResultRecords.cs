using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkyDash.vCloud.Response
{
    //get and set snapshot XML object
    [XmlRoot(ElementName = "Snapshot", Namespace = "http://www.vmware.com/vcloud/v1.5")]
    public class Snapshot
    {
        public int accountId;
        [XmlAttribute(AttributeName = "created")]
        public DateTime Created { get; set; }
        [XmlAttribute(AttributeName = "poweredOn")]
        public string PoweredOn { get; set; }
        [XmlAttribute(AttributeName = "size")]
        public string Size { get; set; }
        public long SizeInGB;
    }
    //get and set detailed info for snapshotsection object from XML 
    [XmlRoot(ElementName = "SnapshotSection", Namespace = "http://www.vmware.com/vcloud/v1.5")]
    public class SnapshotSection
    {
        [XmlElement(ElementName = "Info", Namespace = "http://schemas.dmtf.org/ovf/envelope/1")]
        public string Info { get; set; }
        [XmlElement(ElementName = "Snapshot", Namespace = "http://www.vmware.com/vcloud/v1.5")]
        public Snapshot Snapshot { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "ovf", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ovf { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "required", Namespace = "http://schemas.dmtf.org/ovf/envelope/1")]
        public string Required { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
    }
    // 
    [XmlRoot(ElementName = "Link", Namespace = "http://www.vmware.com/vcloud/v1.5")]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
    //Detailed object from VM record XML - derives from queryResultRecords
    [XmlRoot(ElementName = "VMRecord", Namespace = "http://www.vmware.com/vcloud/v1.5")]
    public class VMRecord
    {
        public int skyscapeAccountId;
        public int unofficialId;
        public SnapshotSection Snapshot = null;        
        [XmlAttribute(AttributeName = "container")]
        public string Container { get; set; }
        [XmlAttribute(AttributeName = "containerName")]
        public string ContainerName { get; set; }
        [XmlAttribute(AttributeName = "guestOs")]
        public string GuestOs { get; set; }
        [XmlAttribute(AttributeName = "hardwareVersion")]
        public string HardwareVersion { get; set; }
        [XmlAttribute(AttributeName = "isBusy")]
        public string IsBusy { get; set; }
        [XmlAttribute(AttributeName = "isDeleted")]
        public string IsDeleted { get; set; }
        [XmlAttribute(AttributeName = "isDeployed")]
        public string IsDeployed { get; set; }
        [XmlAttribute(AttributeName = "isInMaintenanceMode")]
        public string IsInMaintenanceMode { get; set; }
        [XmlAttribute(AttributeName = "isPublished")]
        public string IsPublished { get; set; }
        [XmlAttribute(AttributeName = "isVAppTemplate")]
        public string IsVAppTemplate { get; set; }
        [XmlAttribute(AttributeName = "memoryMB")]
        public string MemoryMB { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "numberOfCpus")]
        public string NumberOfCpus { get; set; }
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
        [XmlAttribute(AttributeName = "vdc")]
        public string Vdc { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "isVdcEnabled")]
        public string IsVdcEnabled { get; set; }
        [XmlAttribute(AttributeName = "pvdcHighestSupportedHardwareVersion")]
        public string PvdcHighestSupportedHardwareVersion { get; set; }
        [XmlAttribute(AttributeName = "taskStatus")]
        public string TaskStatus { get; set; }
        [XmlAttribute(AttributeName = "task")]
        public string Task { get; set; }
        [XmlAttribute(AttributeName = "taskDetails")]
        public string TaskDetails { get; set; }
        [XmlAttribute(AttributeName = "vmToolsVersion")]
        public string VmToolsVersion { get; set; }
        [XmlAttribute(AttributeName = "networkName")]
        public string NetworkName { get; set; }
        [XmlAttribute(AttributeName = "taskStatusName")]
        public string TaskStatusName { get; set; }
        [XmlAttribute(AttributeName = "catalogName")]
        public string CatalogName { get; set; }
    }
    //Corresponds to API VM call in XML
    [XmlRoot(ElementName = "QueryResultRecords", Namespace = "http://www.vmware.com/vcloud/v1.5")]
    public class QueryResultRecords
    {
        public int vCloudId;
        [XmlElement(ElementName = "Link", Namespace = "http://www.vmware.com/vcloud/v1.5")]
        public List<Link> Link { get; set; }
        [XmlElement(ElementName = "VMRecord", Namespace = "http://www.vmware.com/vcloud/v1.5")]
        public List<VMRecord> VMRecord { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "page")]
        public string Page { get; set; }
        [XmlAttribute(AttributeName = "pageSize")]
        public string PageSize { get; set; }
        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
    }
}