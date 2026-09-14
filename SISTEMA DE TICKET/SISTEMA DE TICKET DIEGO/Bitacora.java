import java.util.Date;

public class Bitacora {
    private Date dateFechaHora;
    private String strAccion;
    private String strDetalle;

    public Bitacora(String strAccion, String strDetalle) {
        this.dateFechaHora = new Date();
        this.strAccion = strAccion;
        this.strDetalle = strDetalle;
    }

    @Override
    public String toString() {
        return "[" + dateFechaHora + "] " + strAccion + ": " + strDetalle;
    }
}