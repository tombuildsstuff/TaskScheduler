String.prototype.rtrim=function(){
    return this.replace(/\/+$/,'');
};

var dateString = (new Date()).toISOString();

// setup some paths that we'll need
var config = "grunt.option('config')";
var sshconfig = "grunt.config(['sshconfig', " + config + "])";
var current = "<%= " + sshconfig + ".path %>";
var symlink = "<%= " + sshconfig + ".path.rtrim() %>";
var release = "<%= " + sshconfig + ".releases + (grunt.option('buildNumber') || '" + dateString + "') %>";
var configFiles = current + "Configuration/com/<%= grunt.option('config') %>/*.config";
var logs = current + "logs/";
var lbstatus = "<%= " + sshconfig + ".lbstatus %>";

var task = {
    start: {
        command: "/etc/init.d/TaskScheduler start"
    },
    stop: {
        command: "/etc/init.d/TaskScheduler stop",
        options: {
            ignoreErrors: true
        }
    },
    'make-release-dir': {
        command: "mkdir -m 777 -p " + release + "/logs"
    },
    'update-symlinks': {
        command: "rm -rf " + symlink + " && ln -s " + release + " " + symlink
    },
    'set-config': {
        command: "mv -f " + configFiles + " " + current
    },
    'take-app-offline':{
        command: "echo 'OFF' > " + lbstatus,
        options: {
            ignoreErrors: true
        }
    },
    'put-app-online':{
        command: "echo 'ON' > " + lbstatus,
        options: {
            ignoreErrors: true
        }
    }
}

module.exports = task;
