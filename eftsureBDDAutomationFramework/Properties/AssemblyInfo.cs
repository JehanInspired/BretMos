using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TakealotBDDAutomationFramework")]


//added for parallel test execution when using Nunit
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: FixtureLifeCycle(LifeCycle.SingleInstance)]//using InstancePerTestCase is only possible with static OneTimeSetUp and OneTimeTearDown 
[assembly: LevelOfParallelism(5)] //when using seleniumgrid/ remotewebdriver make sure number of sessions is the same or more than this level of parallelism

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
