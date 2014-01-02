var task = {
    runUnitTests: {
        cmd: "mono build/nunit/bin/nunit-console.exe --xml=UnitTestsResult.xml TaskSchedulerTests/bin/<%= grunt.option('config') || 'Release' %>/TaskScheduler.UnitTests.dll --exclude=Database"
    },
    runIntegrationTests: {
        //cmd: "mono build/nunit/bin/nunit-console.exe --xml=IntegrationTestsResult.xml IntegrationTests/bin/<%= grunt.option('config') || 'Release' %>/TaskScheduler.IntegrationTests.dll --exclude=Database"
    }
};

module.exports = task;
