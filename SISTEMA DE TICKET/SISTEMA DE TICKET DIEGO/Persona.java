public abstract class Persona {
    private String strNombre;
    private String strCorreo;
    private String strTelefono;

    public Persona(String strNombre, String strCorreo, String strTelefono) {
        this.strNombre = strNombre;
        this.strCorreo = strCorreo;
        this.strTelefono = strTelefono;
    }

    public String getStrNombre() { return strNombre; }
    public String getStrCorreo() { return strCorreo; }
    public String getStrTelefono() { return strTelefono; }
}
