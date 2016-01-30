myselflogApp.factory('logger', function (toastr, $log) {

    toastr.options.timeOut = 2000; // 2 second toast timeout
    toastr.options.positionClass = 'toast-bottom-right';

    var logger = {
        error: error,
        info: info,
        success: success,
        warning: warning,
        log: log // straight to console; bypass toast
    };

    function error(message) {
        toastr.error(message);
        $log.log(message);
    }
    
    function info(message) {
        toastr.info(message);
        $log.log(message);
    }
    
    function success(message) {
        toastr.success(message);
        $log.log(message);
    }
    
    function warning(message) {
        toastr.warning(message);
        $log.log(message);
    }
    
    function log(message) {
        $log.log(message);
    }

    return logger;
});