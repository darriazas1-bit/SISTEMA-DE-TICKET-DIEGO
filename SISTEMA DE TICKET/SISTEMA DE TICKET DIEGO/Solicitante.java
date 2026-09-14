public class Solicitante extends Persona {
    private String strDepartamento;

    public Solicitante(String strNombre, String strCorreo, String strTelefono, String strDepartamento) {
        super(strNombre, strCorreo, strTelefono);
        this.strDepartamento = strDepartamento;
    }

    public String getStrDepartamento() { return strDepartamento; }
}