<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" name="Reddit" generation="1" functional="0" release="0" Id="39092587-080c-4569-9768-bc2a17cdda48" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="RedditGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HealthStatusService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Reddit/RedditGroup/LB:HealthStatusService:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="NotificationService:health-monitoring" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/Reddit/RedditGroup/LB:NotificationService:health-monitoring" />
          </inToChannel>
        </inPort>
        <inPort name="RedditService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Reddit/RedditGroup/LB:RedditService:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="RedditService:health-monitoring" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/Reddit/RedditGroup/LB:RedditService:health-monitoring" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapHealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthMonitoringServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapHealthMonitoringServiceInstances" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapHealthStatusService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapHealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HealthStatusServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapHealthStatusServiceInstances" />
          </maps>
        </aCS>
        <aCS name="NotificationService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapNotificationService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapNotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="NotificationServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapNotificationServiceInstances" />
          </maps>
        </aCS>
        <aCS name="RedditService:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapRedditService:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="RedditService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapRedditService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="RedditServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Reddit/RedditGroup/MapRedditServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HealthStatusService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Reddit/RedditGroup/HealthStatusService/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:NotificationService:health-monitoring">
          <toPorts>
            <inPortMoniker name="/Reddit/RedditGroup/NotificationService/health-monitoring" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:RedditService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Reddit/RedditGroup/RedditService/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:RedditService:health-monitoring">
          <toPorts>
            <inPortMoniker name="/Reddit/RedditGroup/RedditService/health-monitoring" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHealthMonitoringService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/HealthMonitoringService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHealthMonitoringServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/HealthMonitoringServiceInstances" />
          </setting>
        </map>
        <map name="MapHealthStatusService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/HealthStatusService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapHealthStatusService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/HealthStatusService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHealthStatusServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/HealthStatusServiceInstances" />
          </setting>
        </map>
        <map name="MapNotificationService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/NotificationService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/NotificationService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapNotificationServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/NotificationServiceInstances" />
          </setting>
        </map>
        <map name="MapRedditService:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/RedditService/DataConnectionString" />
          </setting>
        </map>
        <map name="MapRedditService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Reddit/RedditGroup/RedditService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapRedditServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/RedditServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HealthMonitoringService" generation="1" functional="0" release="0" software="C:\Users\Jovan\Desktop\RedditProject\Reddit\Reddit\csx\Debug\roles\HealthMonitoringService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthMonitoringService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot; /&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;RedditService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/HealthMonitoringServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Reddit/RedditGroup/HealthMonitoringServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Reddit/RedditGroup/HealthMonitoringServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="HealthStatusService" generation="1" functional="0" release="0" software="C:\Users\Jovan\Desktop\RedditProject\Reddit\Reddit\csx\Debug\roles\HealthStatusService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="15008" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HealthStatusService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot; /&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;RedditService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/HealthStatusServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Reddit/RedditGroup/HealthStatusServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Reddit/RedditGroup/HealthStatusServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="NotificationService" generation="1" functional="0" release="0" software="C:\Users\Jovan\Desktop\RedditProject\Reddit\Reddit\csx\Debug\roles\NotificationService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="health-monitoring" protocol="tcp" portRanges="10100" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;NotificationService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot; /&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;RedditService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/NotificationServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Reddit/RedditGroup/NotificationServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Reddit/RedditGroup/NotificationServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="RedditService" generation="1" functional="0" release="0" software="C:\Users\Jovan\Desktop\RedditProject\Reddit\Reddit\csx\Debug\roles\RedditService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="14543" />
              <inPort name="health-monitoring" protocol="tcp" portRanges="10110" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;RedditService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HealthMonitoringService&quot; /&gt;&lt;r name=&quot;HealthStatusService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;NotificationService&quot;&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;RedditService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;health-monitoring&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Reddit/RedditGroup/RedditServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Reddit/RedditGroup/RedditServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Reddit/RedditGroup/RedditServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="RedditServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthStatusServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="NotificationServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="HealthMonitoringServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HealthMonitoringServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="HealthStatusServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="NotificationServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="RedditServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HealthMonitoringServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="HealthStatusServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="NotificationServiceInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="RedditServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d4394a08-8e97-4bdf-9cff-ae8bb3200adc" ref="Microsoft.RedDog.Contract\ServiceContract\RedditContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="4a85dd92-b9ce-4824-b5cf-88d30cfebf70" ref="Microsoft.RedDog.Contract\Interface\HealthStatusService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Reddit/RedditGroup/HealthStatusService:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="ad52762d-9b4b-4fc1-9ec4-c23bd477e8bb" ref="Microsoft.RedDog.Contract\Interface\NotificationService:health-monitoring@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Reddit/RedditGroup/NotificationService:health-monitoring" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="9ebec729-e268-44d2-ac04-1d5607cfa62a" ref="Microsoft.RedDog.Contract\Interface\RedditService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Reddit/RedditGroup/RedditService:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="5fe265af-81e7-477e-8bb2-cccb524bd504" ref="Microsoft.RedDog.Contract\Interface\RedditService:health-monitoring@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Reddit/RedditGroup/RedditService:health-monitoring" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>