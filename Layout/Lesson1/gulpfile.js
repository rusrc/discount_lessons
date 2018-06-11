var gulp = require("gulp")
    , gulp_concat = require("gulp-concat")
	, gulp_sass = require("gulp-sass")
    , fs = require("fs");

const contentFolder = "content";


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

gulp.task('sass', function () 
{
  //./sass/**/*.scss
  gulp.src('sass/style.scss')
    .pipe(gulp_sass({outputStyle: 'compressed'}).on('error', gulp_sass.logError))
    .pipe(gulp.dest('./css'));
});

tasks.push("sass");

gulp.task("default", tasks, function ()
{
	//./sass/**/*.scss
    gulp.watch([`${contentFolder}/*.html`, "*.html", "sass/style.scss"], tasks);
});

