function write_text(text = '', clear = false, remove_last = false) {
    let calc_inp = document.getElementById('calculator_input');
    if (clear) {
        calc_inp.value = '';
    } else if (remove_last) {
        calc_inp.value = calc_inp.value.slice(0, -1);
    }
    else if (text) {
        calc_inp.value += text.toString();
    }
};

function change_func_trig_and_log() {
    trig = document.getElementsByClassName('trigonometry');
    for (let item of trig) {
        if (item.innerHTML[0] == "a") {
            item.innerHTML = item.innerHTML.slice(1) + "h";
        } else if (item.innerHTML[item.innerHTML.length - 1] == "h") {
            item.innerHTML = item.innerHTML.slice(0, -1);
        }
        else {
            item.innerHTML = "a" + item.innerHTML;
        }
        item.setAttribute("onClick", "write_text('" + item.innerHTML + "(');");
    }
    l = document.getElementById('logarithm');
    if (l.innerHTML == 'log') {
        l.innerHTML = 'log2';
    } else if (l.innerHTML == 'log2') {
        l.innerHTML = 'log10';
    } else if (l.innerHTML == 'log10') {
        l.innerHTML = 'log';
    } else {
        l.innerHTML = 'log';
    }
    l.setAttribute("onClick", "write_text('" + l.innerHTML + "(');");
}

function count_expression() {
    let calc_inp = document.getElementById('calculator_input');
    if (calc_inp.value == '') {
        return
    }
    calc_inp.value = calc_inp.value.replace(',','.');
    try {
        let parsed = math.parse(calc_inp.value);
        calc_inp.value = math.format(parsed.compile().evaluate());
    }
    catch (err) {
        alert(err);
    }
};
