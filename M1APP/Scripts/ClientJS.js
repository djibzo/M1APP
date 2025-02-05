$(document).ready(function () {
    console.log("ClientJS.js loaded");

    loadClients();
});

function loadClients() {
    $.ajax({
        url: '/ClientAjax/List',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#clientTableBody').empty(); // Vider le corps du tableau avant de le remplir

            if (data.length === 0) {
                $('#clientTableBody').append('<tr><td colspan="6" style="text-align: center; padding: 12px; color: red;">Aucun client trouvé.</td></tr>');
            } else {
                $.each(data, function (index, client) {
                    $('#clientTableBody').append(
                        '<tr style="border-bottom: 1px solid #ddd; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#f9f9f9\'" onmouseout="this.style.backgroundColor=\'white\'">' +
                        '<td style="padding: 12px; text-align: left;">' + client.CniClient + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + client.NomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + client.PrenomUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + client.EmailUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' + client.TelUtilisateur + '</td>' +
                        '<td style="padding: 12px; text-align: left;">' +
                        '<a href="#" onclick="editClient(' + client.IdUtilisateur + ')" style="background-color: #2196F3; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#1e88e5\'" onmouseout="this.style.backgroundColor=\'#2196F3\'">Edit</a> | ' +
                        '<a href="#" onclick="deleteClient(' + client.IdUtilisateur + ')" style="background-color: #f44336; color: white; padding: 5px 10px; border-radius: 3px; text-decoration: none; transition: background-color 0.3s;" onmouseover="this.style.backgroundColor=\'#e53935\'" onmouseout="this.style.backgroundColor=\'#f44336\'">Delete</a>' +
                        '</td>' +
                        '</tr>'
                    );
                });
            }
        },
        error: function () {
            alert('Erreur lors de la récupération des données des clients.');
        }
    });
}

function addClient() {
    console.log("addClient function called");

    var client = {
        CniClient: $('#addClientForm').find('input[name="CniClient"]').val(),
        NomUtilisateur: $('#addClientForm').find('input[name="NomUtilisateur"]').val(),
        PrenomUtilisateur: $('#addClientForm').find('input[name="PrenomUtilisateur"]').val(),
        EmailUtilisateur: $('#addClientForm').find('input[name="EmailUtilisateur"]').val(),
        TelUtilisateur: $('#addClientForm').find('input[name="TelUtilisateur"]').val(),
        PasswordUtilisateur: $('#addClientForm').find('input[name="PasswordUtilisateur"]').val()
    };

    console.log("Client data:", client);

    $.ajax({
        url: '/ClientAjax/Create',
        type: 'POST',
        data: client,
        success: function (response) {
            console.log("Response from server:", response);
            if (response.success) {
                alert(response.message);
                $('#addClientModal').modal('hide');
                loadClients();
            } else {
                alert(response.message);
                if (response.errors) {
                    console.log("Validation errors:", response.errors);
                }
            }
        },
        error: function () {
            alert('Erreur lors de l\'ajout du client.');
        }
    });
}

function editClient(id) {
    // Récupérer les détails du client et afficher un formulaire de modification
    $.ajax({
        url: '/ClientAjax/Details/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                var client = response.data;
                // Afficher un formulaire de modification avec les données du client
                // Vous pouvez utiliser un modal ou une autre méthode pour afficher le formulaire
                // Exemple de code pour afficher un formulaire de modification
                $('#editClientForm').find('input[name="IdUtilisateur"]').val(client.IdUtilisateur);
                $('#editClientForm').find('input[name="CniClient"]').val(client.CniClient);
                $('#editClientForm').find('input[name="NomUtilisateur"]').val(client.NomUtilisateur);
                $('#editClientForm').find('input[name="PrenomUtilisateur"]').val(client.PrenomUtilisateur);
                $('#editClientForm').find('input[name="EmailUtilisateur"]').val(client.EmailUtilisateur);
                $('#editClientForm').find('input[name="TelUtilisateur"]').val(client.TelUtilisateur);
                $('#editClientModal').modal('show');
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Erreur lors de la récupération des détails du client.');
        }
    });
}

function updateClient() {
    var client = {
        IdUtilisateur: $('#editClientForm').find('input[name="IdUtilisateur"]').val(),
        CniClient: $('#editClientForm').find('input[name="CniClient"]').val(),
        NomUtilisateur: $('#editClientForm').find('input[name="NomUtilisateur"]').val(),
        PrenomUtilisateur: $('#editClientForm').find('input[name="PrenomUtilisateur"]').val(),
        EmailUtilisateur: $('#editClientForm').find('input[name="EmailUtilisateur"]').val(),
        TelUtilisateur: $('#editClientForm').find('input[name="TelUtilisateur"]').val()
    };

    $.ajax({
        url: '/ClientAjax/Edit',
        type: 'POST',
        data: client,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $('#editClientModal').modal('hide');
                loadClients();
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Erreur lors de la mise à jour du client.');
        }
    });
}

function deleteClient(id) {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce client ?')) {
        $.ajax({
            url: '/ClientAjax/Delete/' + id,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    loadClients();
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('Erreur lors de la suppression du client.');
            }
        });
    }
}

// Fonction pour afficher le modal d'ajout de client
function showAddClientModal() {
    $('#addClientModal').modal('show');
}
