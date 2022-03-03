﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// https://sweetalert2.github.io/

// Write your JavaScript code.
$(document).ready(function () {

    $('.dinheiro').mask('000.000.000.000.000,0000', { reverse: true });

    frm_manutencao_delete();

    $("#frm_manutencao_update").submit(function (event) {
        document.getElementById('PrecoVenda').value = AjustaPreco('PrecoVenda');
    });

});

function AjustaPreco(campo) {
    var Conteudo = document.getElementById(campo).value;
    var resultado = "";
    var validacao;
    for (let i = 0; i < Conteudo.length; i = i + 1) {
        validacao = Conteudo.substring(i, i + 1);

        if ('0123456789.,'.indexOf(validacao) != -1) {
            if (validacao == ",") {
                resultado = resultado + ".";
            }
            else if (validacao == ".") {
               
            } else {
                resultado = resultado + validacao;
            }
        }

    }

    //document.getElementById(campo).value = resultado;
    return resultado;
};


function justNumbers(text) {
    var numbers = text.replace(/[^0-9]/g, '');
    return parseInt(numbers);
};


function buscar_cep() {

    var campo = document.getElementById('cep_parceiro');
    campo = justNumbers(campo.value);


    $.ajax({
        url: "https://viacep.com.br/ws/" + campo + "/json/",
        dataType: 'json',
        success: function (response) {
            console.log(response);
            if (response.erro != true) {

                document.getElementById('endereco_parceiro').value = response.logradouro;
                document.getElementById('complemento_parceiro').value = response.complemento;
                document.getElementById('bairro_parceiro').value = response.bairro;
                document.getElementById('cidade_parceiro').value = response.localidade;
            }
        }
    });
}

function frm_manutencao_delete() {
    $(".gestor-btn-excluir").click(function (e) {

        e.preventDefault();

        Swal.fire({
            title: 'Deseja Realmente Excluir?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
        }).then((result) => {
            if (result.isConfirmed) {

                Swal.fire(
                    {
                        title: 'Excluido!',
                        text: 'Registro Excluido do Sistema.',
                        icon: 'success'
                    }
                );

                document.getElementById("frm_manutencao_delete").submit();
            }
        });
    })
};


