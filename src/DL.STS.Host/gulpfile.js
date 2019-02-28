/// <binding BeforeBuild='clean' AfterBuild='default' Clean='clean' />
let gulp = require('gulp'),
    rimraf = require('rimraf'),
    paths = {
        nodeModuleRoot: './node_modules/',
        contentRoot: './assets/css/',
        scriptRoot: './assets/scripts/',
        imagesRoot: './assets/images/',
        webRoot: './wwwroot/'
    };

// images
gulp.task('copy:images', function () {
    return gulp.src(paths.imagesRoot + '**/*')
        .pipe(gulp.dest(paths.webRoot + 'images/'));
});
gulp.task('clean:images', function (callback) {
    rimraf(paths.webRoot + 'images', callback);
});

// font-awesome
gulp.task('copy:font-awesome-styles', function () {
    return gulp.src(paths.nodeModuleRoot + '@fortawesome/fontawesome-free/css/all.*')
        .pipe(gulp.dest(paths.webRoot + 'libs/fontawesome/'));
});
gulp.task('copy:font-awesome-fonts', function () {
    return gulp.src(paths.nodeModuleRoot + '@fortawesome/fontawesome-free/webfonts/**/*')
        .pipe(gulp.dest(paths.webRoot + 'libs/webfonts/'));
});

// libs
gulp.task('clean:libs', function (callback) {
    rimraf(paths.webRoot + 'libs', callback);
});

// main tasks
gulp.task('default', [
    'copy:images',
    'copy:font-awesome-styles',
    'copy:font-awesome-fonts'
]);

gulp.task('clean', [
    'clean:images',
    'clean:libs'
]);