$(document).ready(function () {
    console.log("adminJS.js loaded");

    loadadmins();

 
});

function loadadmins() {
    $.ajax({
        url: '/AdminAjax/List',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#adminTableBody').empty(); // Vider le corps du tableau avant de le remplir

            if (data.length === 0) {
                $('#adminTableBody').append('<tr><td colspan="6" style="text-align: center; padding: 12px; color: red;">Aucun admin trouvé.</td></tr>');
            } else {
                $.each(data, function (index, admin) {
                    $('#adminTableBody').append(
                        '<tr style="border-bottom: 1px solid #ddd; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#f9f9f9\'" onmouseout="this.style.backgroundColor=\'white\'">' +
                        '<td style="padding: 12px; text-align: left;">' + admin.MatriculeAdmin + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.NomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.PrenomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.EmailUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.TelUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' +
                        '<a href="#" onclick="editAdmin(' + admin.IdUtilisateur + ')" style="background-color: #2196F3; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#1e88e5\'" onmouseout="this.style.backgroundColor=\'#2196F3\'">Edit</a> | ' +
                        '<a href="#" onclick="deleteadmin(' + admin.IdUtilisateur + ')" style="background-color: #f44336; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#e53935\'" onmouseout="this.style.backgroundColor=\'#f44336\'">Delete</a>' +
                        '</td>' +
                        '</tr>'
                    );
                });
            }
        },
        error: function () {
            alert('Erreur lors de la récupération des données des admins.');
        }
    });
}

function addadmin() {
    console.log("addadmin function called");

    var admin = {
        MatriculeAdmin: $('#addadminForm').find('input[name="MatriculeAdmin"]').val(),
        NomUtilisateur: $('#addadminForm').find('input[name="NomUtilisateur"]').val(),
        PrenomUtilisateur: $('#addadminForm').find('input[name="PrenomUtilisateur"]').val(),
        EmailUtilisateur: $('#addadminForm').find('input[name="EmailUtilisateur"]').val(),
        TelUtilisateur: $('#addadminForm').find('input[name="TelUtilisateur"]').val(),
        PasswordUtilisateur: $('#addadminForm').find('input[name="PasswordUtilisateur"]').val()
    };

    console.log("admin data:", admin);

    $.ajax({
        url: '/AdminAjax/Create',
        type: 'POST',
        data: admin,
        success: function (response) {
            console.log("Response from server:", response);
            if (response.success) {
                alert(response.message);
                $('#addadminModal').modal('hide');
                loadadmins();
            } else {
                alert(response.message);
                if (response.errors) {
                    console.log("Validation errors:", response.errors);
                }
            }
        },
        error: function () {
            alert('Erreur lors de l\'ajout du admin.');
        }
    });
}

function editadmin(id) {
    // Récupérer les détails du admin et afficher un formulaire de modification
    $.ajax({
        url: '/AdminAjax/Details/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                var admin = response.data;
                // Afficher un formulaire de modification avec les données du admin
                // Vous pouvez utiliser un modal ou une autre méthode pour afficher le formulaire
                // Exemple de code pour afficher un formulaire de modification
                $('#editadminForm').find('input[name="IdUtilisateur"]').val(admin.IdUtilisateur);
                $('#editadminForm').find('input[name="Cniadmin"]').val(admin.Cniadmin);
                $('#editadminForm').find('input[name="NomUtilisateur"]').val(admin.NomUtilisateur);
                $('#editadminForm').find('input[name="PrenomUtilisateur"]').val(admin.PrenomUtilisateur);
                $('#editadminForm').find('input[name="EmailUtilisateur"]').val(admin.EmailUtilisateur);
                $('#editadminForm').find('input[name="TelUtilisateur"]').val(admin.TelUtilisateur);
                $('#editadminModal').modal('show');
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Erreur lors de la récupération des détails du admin.');
        }
    });
}

function updateadmin() {
    var admin = {
        IdUtilisateur: $('#editadminForm').find('input[name="IdUtilisateur"]').val(),
        Cniadmin: $('#editadminForm').find('input[name="Cniadmin"]').val(),
        NomUtilisateur: $('#editadminForm').find('input[name="NomUtilisateur"]').val(),
        PrenomUtilisateur: $('#editadminForm').find('input[name="PrenomUtilisateur"]').val(),
        EmailUtilisateur: $('#editadminForm').find('input[name="EmailUtilisateur"]').val(),
        TelUtilisateur: $('#editadminForm').find('input[name="TelUtilisateur"]').val()
    };

    $.ajax({
        url: '/AdminAjax/Edit',
        type: 'POST',
        data: admin,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $('#editadminModal').modal('hide');
                loadadmins();
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Erreur lors de la mise à jour du admin.');
        }
    });
}

function deleteadmin(id) {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce admin ?')) {
        $.ajax({
            url: '/AdminAjax/Delete/' + id,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    loadadmins();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Erreur lors de la suppression du admin.');
            }
        });
    }
}

// Fonction pour foecer l'affiage  du  modal d'ajout de admin
function showAddadminModal() {
    $('#addadminModal').modal('show');
}

//vonction pour forcer la fermeture du modal d'ajout de admin
function hideAddadminModal() {
    $('#addadminModal').modal('hide');
    }


function searchadmins() {
    var query = $('#searchadminInput').val();
    if (query.length > 2) {
        $.ajax({
            url: '/AdminAjax/Search',
            type: 'GET',
            data: { query: query },
            dataType: 'json',
            success: function (data) {
                $('#suggestionsList').empty();
                if (data.length > 0) {
                    $.each(data, function (index, admin) {
                        $('#suggestionsList').append(
                            '<li class="list-group-item list-group-item-action" onclick="selectadmin(\'' + admin.MatriculeAdmin + '\')">' +
                            admin.MatriculeAdmin+' - ' + admin.NomUtilisateur + ' ' + admin.PrenomUtilisateur +
                            '</li>'
                        );
                    });
                } else {
                    $('#suggestionsList').append('<li class="list-group-item">Aucun admin trouvé.</li>');
                }
            },
            error: function () {
                $('#suggestionsList').empty();
                $('#suggestionsList').append('<li class="list-group-item">Erreur lors de la recherche.</li>');
            }
        });
    } else {
        $('#suggestionsList').empty();
        // Recharger la liste complète des admins si le champ de recherche est vide
        loadadmins(); 
    }
}

function selectadmin(criteria) {
    $('#searchadminInput').val(criteria);
    $('#suggestionsList').empty();

    $.ajax({
        url: '/AdminAjax/Search',
        type: 'GET',
        data: { query: criteria },
        dataType: 'json',
        success: function (data) {
            $('#adminTableBody').empty(); // Vider le corps du tableau avant de le remplir

            if (data.length === 0) {
                $('#adminTableBody').append('<tr><td colspan="6" style="text-align: center; padding: 12px; color: red;">Aucun admin trouvé.</td></tr>');
            } else {
                $.each(data, function (index, admin) {
                    $('#adminTableBody').append(
                        '<tr style="border-bottom: 1px solid #ddd; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#f9f9f9\'" onmouseout="this.style.backgroundColor=\'white\'">' +
                        '<td style="padding: 12px; text-align: left;">' + admin.MatriculeAdmin + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.NomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.PrenomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.EmailUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + admin.TelUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' +
                        '<a href="#" onclick="editadmin(' + admin.IdUtilisateur + ')" style="background-color: #2196F3; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#1e88e5\'" onmouseout="this.style.backgroundColor=\'#2196F3\'">Edit</a> | ' +
                        '<a href="#" onclick="deleteadmin(' + admin.IdUtilisateur + ')" style="background-color: #f44336; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#e53935\'" onmouseout="this.style.backgroundColor=\'#f44336\'">Delete</a>' +
                        '</td>' +
                        '</tr>'
                    );
                });
            }
        },
        error: function () {
            alert('Erreur lors de la récupération des données des admins.');
        }
    });
}
