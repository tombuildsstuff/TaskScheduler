var task = function(grunt){

    grunt.registerTask('watch-unitTests', function(){
        grunt.log.write("##teamcity[importData type='nunit' path='./UnitTestsResult.xml']");
    });

    grunt.registerTask('watch-integrationTests', function(){
        grunt.log.write("##teamcity[importData type='nunit' path='./IntegrationTestsResult.xml']");
    });
};

module.exports = task;
