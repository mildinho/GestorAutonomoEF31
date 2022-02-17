// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {


    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });

    BotaoExcluir();

});

function BotaoExcluir() {
    $(".gestor-btn-excluir").click(function (e) {
       /* https://sweetalert2.github.io/
        * var resultado = confirm("Tem certeza que deseja realizar esta operação?");
        if (resultado == false) {
            e.preventDefault();
     
       }
      

       */
        e.preventDefault();


        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true, showCancelButton: true,
            confirmButtonText: `Save`,
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                Swal.fire('Saved!', '', 'success');
                document.getElementById("ManutencaoXX").submit();
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        });
        



        

    });
}