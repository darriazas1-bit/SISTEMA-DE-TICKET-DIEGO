public class Tecnico extends Persona {
    private String strEspecialidad;
    private int intCargaActual;
    private int intCargaMaxima;

    public Tecnico(String strNombre, String strCorreo, String strTelefono, String strEspecialidad, int intCargaMaxima) {
        super(strNombre, strCorreo, strTelefono);
        this.strEspecialidad = strEspecialidad;
        this.intCargaMaxima = intCargaMaxima;
        this.intCargaActual = 0;
    }

    public boolean tieneCapacidad() { return intCargaActual < intCargaMaxima; }
    public void aumentarCarga() { if (tieneCapacidad()) intCargaActual++; }
    public void reducirCarga() { if (intCargaActual > 0) intCargaActual--; }
    public int getIntCargaActual() { return intCargaActual; }
}