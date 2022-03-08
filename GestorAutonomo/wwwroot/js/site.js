// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { Swal } = require("../lib/limonte-sweetalert2/sweetalert2");


// https://sweetalert2.github.io/

// Write your JavaScript code.
$(document).ready(function () {

    $('.dinheiro').mask('000.000.000.000.000,0000', { reverse: true });

    frm_manutencao_delete();

    $("#frm_manutencao_update").submit(function (event) {
        document.getElementById('PrecoVenda').value = AjustaPreco('PrecoVenda');
        document.getElementById('PrecoMedio').value = AjustaPreco('PrecoMedio');
        document.getElementById('PrecoCusto').value = AjustaPreco('PrecoCusto');
    });

    AjaxUploadImagemProduto();

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
            } else if (response.erro == true) {
                Swal.fire(
                    {
                        title: 'CEP Não Encontrado!',
                        text: 'Verifique o CEP Informado!',
                        icon: 'error'
                    }
                );
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


function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).parent().parent().find(".input-file").click();
    });
    $(".btn-imagem-excluir").click(function () {
        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var InputFile = $(this).parent().find(".input-file");

        $.ajax({
            type: "GET",
            url: "/Admin/Imagem/Deletar?caminho=" + CampoHidden.val(),
            error: function () {

            },
            success: function () {
                Imagem.attr("src", "/images/imagem-padrao.png");
                BtnExcluir.addClass("btn-ocultar");
                CampoHidden.val("");
                InputFile.val("");
            }
        });
    });

    $(".input-file").change(function () {
        //Formulário de dados via JavaScript
        var Binario = $(this)[0].files[0];
        var Formulario = new FormData();
        Formulario.append("file", Binario);

        var CampoHidden = $(this).parent().find("input[name=imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");

        //Apresenta Imagem Loading.
        Imagem.attr("src", "/images/loading.gif");
        Imagem.addClass("thumb");

        //TODO - Requisição Ajax enviado a Formulario
        $.ajax({
            type: "POST",
            headers: {
                "RequestVerificationToken": gettoken()
            },
            url: "/Admin/Imagem/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert("Erro no envio do arquivo!");
                Imagem.attr("src", "/images/imagem-padrao.png");
                Imagem.removeClass("thumb");
            },
            success: function (data) {
                var caminho = data.caminho;
                Imagem.attr("src", caminho);
                Imagem.removeClass("thumb");
                CampoHidden.val(caminho);
                BtnExcluir.removeClass("btn-ocultar");
            }
        });
    });
}
