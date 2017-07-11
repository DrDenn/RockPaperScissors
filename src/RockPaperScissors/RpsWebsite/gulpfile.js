/// <binding BeforeBuild='default' />

var gulp = require('gulp');

var project = {
    webroot: "wwwroot"
};

var paths = {
    webroot: "./" + project.webroot + "/",
    lib: "./" + project.webroot + "/lib/",
    scripts: "./" + project.webroot + "/lib/scripts/"
};

gulp.task('default', function () {
    gulp.src("./Scripts/*.js")
        .pipe(gulp.dest(paths.scripts));
});