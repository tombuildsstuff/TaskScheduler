var task = {
  warmup:
    options: {
       url: 'http://<%= grunt.option("server") %>/',
       timeout: 60000
  }
};

module.exports = task;
