var gulp = require("gulp")
, gulp_concat = require("gulp-concat")
, gulp_minify_css = require("gulp-minify-css")
, gulp_replace = require("gulp-replace")
, gulp_less = require("gulp-less")
, gulp_watch = require("gulp-watch");

var pathFrom = "less";
var pathTo = "dist/css";

var lessStylesArr = [
    "less/mixins/variables.less",
    "less/mixins/_variants.less",
    "less/general.less",
    "less/font.less",
    "less/*.less",     
    "less/app/*/*.less"   
];


var proccessLess = function (lessStyles, whereToPut, resultedFileName) {
    gulp.src(lessStyles)
        .pipe(gulp_concat(resultedFileName))
        .pipe(gulp_less().on('error', function(e){
            console.log(e);
            gulp_less.end();
        }))
        //.pipe(gulp_minify_css())
        .pipe(gulp_replace(pathFrom, pathTo))
        .pipe(gulp.dest(whereToPut));
    
    return gulp;
}


gulp.task("lessConcat", function(){
    return proccessLess(lessStylesArr, pathTo, "style.min.css");
});

gulp.task("w", function () {
    gulp.watch("less/**/*.less", ["lessConcat"]);
});
