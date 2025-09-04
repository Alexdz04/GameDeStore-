// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const elementsToFadeIn = document.querySelectorAll('.jumbotron, .container h2, .table, .card');

    elementsToFadeIn.forEach(function (element, index) {
        element.style.opacity = 0;
        element.style.transition = `opacity 0.5s ease-in-out ${index * 0.1}s`;

        setTimeout(() => {
            element.style.opacity = 1;
        }, 100);
    });
});

