
import java.util.Date;

public class FlujoTicket {
    private Date dateFechaCambio;
    private String strEstadoAnterior;
    private String strEstadoNuevo;
    private String strResponsable;

    public FlujoTicket(String strEstadoAnterior, String strEstadoNuevo, String strResponsable) {
        this.dateFechaCambio = new Date();
        this.strEstadoAnterior = strEstadoAnterior;
        this.strEstadoNuevo = strEstadoNuevo;
        this.strResponsable = strResponsable;
    }

    @Override
    public String toString() {
        return "[" + dateFechaCambio + "] Cambio de Estado: " + strEstadoAnterior + " -> " + strEstadoNuevo + " (Por: " + strResponsable + ")";
    }
}