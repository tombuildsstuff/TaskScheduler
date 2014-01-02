var task = {
    vagrant : {
        host: "0.0.0.0",
        port: 2222,
        username: "otdeploy",
        password: "vagrant",
        path: '/var/www/taskscheduler/current/',
        releases:'/var/www/taskscheduler/releases/',
        lbstatus: '/etc/lbstatus/taskscheduler',
        httpPort: 3000
    },
    'qa-test': {
        host: "<%= grunt.option('server') %>",
        port: "<%= (grunt.option('port') || 22) %>",
        username: "<%= grunt.option('username') %>",
        password: "<%= grunt.option('password') %>",
        path: '/var/www/taskscheduler/current/',
        releases:'/var/www/taskscheduler/releases/',
        lbstatus: '/etc/lbstatus/taskscheduler',
        httpPort: 8080
    },
    production: {
    host: "<%= grunt.option('server') %>",
        port: "<%= (grunt.option('port') || 22) %>",
        username: "<%= grunt.option('username') %>",
        password: "<%= grunt.option('password') %>",
        path: '/var/www/taskscheduler/current/',
        releases:'/var/www/taskscheduler/releases/',
        lbstatus: '/etc/lbstatus/taskscheduler',
        httpPort: 80
    }
}

module.exports = task;
