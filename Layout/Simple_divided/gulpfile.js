var gulp = require("gulp")
    , gulp_concat = require("gulp-concat")
    , gulp_minify_css = require("gulp-minify-css")
    , gulp_replace = require("gulp-replace")
    , gulp_less = require("gulp-less")
    , gulp_watch = require("gulp-watch")
    , fs = require("fs");

const pathFrom = "less";
const pathTo = "dist/css";
const contentFolder = "content";

var lessStylesArr = [
    "less/mixins/variables.less",
    "less/mixins/_variants.less",
    "less/general.less",
    "less/font.less",
    "less/*.less",
    "less/app/*/*.less"
];

var proccessLess = function (lessStyles, whereToPut, resultedFileName)
{
    gulp.src(lessStyles)
        .pipe(gulp_concat(resultedFileName))
        .pipe(gulp_less().on('error', function (e)
        {
            console.log(e);
            gulp_less.end();
        }))
        //.pipe(gulp_minify_css())
        .pipe(gulp_replace(pathFrom, pathTo))
        .pipe(gulp.dest(whereToPut));

    return gulp;
}

var files = fs.readdirSync(contentFolder);
var tasks = [];

files.forEach(fileName =>
{
    let taskName = fileName.replace(".html", "");
    let fileList = [];

    fileList.push("header.html");
    fileList.push(`${contentFolder}/${fileName}`);
    fileList.push("footer.html");

    gulp.task(taskName, () =>
    {
        return gulp
            .src(fileList)
            .pipe(gulp_concat(fileName))
            .pipe(gulp.dest("./build/"));
    });

    tasks.push(taskName);
});

// gulp.task("second", () =>
// {
//     return gulp.src("styles/style.css")
//         .pipe(gulp_replace("styles", "./build"))
//         .pipe(gulp.dest("./build"));
// });
//tasks.push("second");

gulp.task("lessConcat", function ()
{
    return proccessLess(lessStylesArr, pathTo, "style.min.css");
});

gulp.task("w", function ()
{
    gulp.watch("less/**/*.less", ["lessConcat"]);
});

gulp.task("default", tasks, function ()
{
    gulp.watch([`${contentFolder}/*.html`, "*.html"], tasks);
});

