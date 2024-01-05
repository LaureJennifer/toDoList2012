
function CreateCircularPercent(label, elementId, elementCircleId, counter, unit, total) {
    let element = document.querySelector(elementId);
    let counterValue = 0;
    const intervalId = setInterval(() => {
        if (counterValue === counter) {
            clearInterval(intervalId);
        } else {
            counterValue += 1;
            const percentage = (counterValue / total) * 100;
            element.innerHTML = label + "<br/>" + percentage.toFixed(2) + unit;

            const offset = (percentage / 100) * total;
            const circle = document.querySelector(elementCircleId);
            const circumference = 2 * Math.PI * circle.getAttribute("r");
            const dashOffset = circumference - (offset / total) * circumference;

            circle.style.strokeDasharray = circumference;
            circle.style.strokeDashoffset = dashOffset;
        }
    }, 20);
}
function CreateCircular(label, elementId, elementCircleId, counter, unit, total) {
    let element = document.querySelector(elementId);
    let counterValue = 0;
    var dvi = 100000;

    if (counter > 1000000) {
        dvi = 100000;
    }
    else
        if (counter > 100000) {
            dvi = 10000;
        }
        else
            if (counter > 10000) {
                dvi = 1000;
            }
            else
                if (counter > 1000) {
                    dvi = 100;
                }
                else
                    if (counter > 100) {
                        dvi = 2;
                    }
                    else {
                        dvi = 1;
                    }
    const intervalId = setInterval(() => {
        if (counter === 0 || counterValue === counter) {
            clearInterval(intervalId);
        } else {
            counterValue += dvi;
            const percentage = (counterValue / total) * 100;
            element.innerHTML = label + "<br/>" + counterValue + unit;

            const offset = (percentage / 100) * total;
            const circle = document.querySelector(elementCircleId);
            const circumference = 2 * Math.PI * circle.getAttribute("r");
            const dashOffset = circumference - (offset / total) * circumference;

            circle.style.strokeDasharray = circumference;
            circle.style.strokeDashoffset = dashOffset;
        }
    }, 20);
}