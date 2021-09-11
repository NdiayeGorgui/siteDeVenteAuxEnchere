function afficher_cacher(id) {
    if (document.getElementById(id).style.visibility == "hidden") {
        document.getElementById(id).style.visibility = "visible";
        document.getElementById('bouton_' + id).innerHTML = 'Cacher la suite';
    }
    else {
        document.getElementById(id).style.visibility = "hidden";
        document.getElementById('bouton_' + id).innerHTML = 'Afficher la suite';
    }
    return true;
}