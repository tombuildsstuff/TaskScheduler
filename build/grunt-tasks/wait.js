var task = {
    options: {
        delay: 2000
    },
    pause: {
        options: {
            before : function(options) {
                console.log('pausing %dms', options.delay);
            },
            after : function() {
                console.log('pause end');
            }
        }
    }
};

module.exports = task;

