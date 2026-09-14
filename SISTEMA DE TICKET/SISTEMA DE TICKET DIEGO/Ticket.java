import java.util.ArrayList;
import java.util.List;

public class Ticket {
    private int intIdTicket;
    private String strTitulo;
    private String strDescripcion;
    private String strEstado; // "Nuevo", "Asignado", "Resuelto", "Cerrado"
    private String strPrioridad;
    private boolean blnEscalado;

    private Solicitante objSolicitante;
    private Tecnico objTecnicoAsignado;
    
    private List<Bitacora> lstBitacora;
    private List<FlujoTicket> lstFlujoTicket;

    public Ticket(int intIdTicket, String strTitulo, String strDescripcion, String strPrioridad, Solicitante objSolicitante) {
        this.intIdTicket = intIdTicket;
        this.strTitulo = strTitulo;
        this.strDescripcion = strDescripcion;
        this.strPrioridad = strPrioridad;
        this.objSolicitante = objSolicitante;
        this.strEstado = "Nuevo";
        this.blnEscalado = false;
        
        this.lstBitacora = new ArrayList<>();
        this.lstFlujoTicket = new ArrayList<>();

        agregarBitacora("CREACION", "Ticket registrado en el sistema.");
        registrarCambioFlujo("Ninguno", "Nuevo", objSolicitante.getStrNombre());
    }

    public void agregarBitacora(String strAccion, String strDetalle) {
        lstBitacora.add(new Bitacora(strAccion, strDetalle));
    }

    public void registrarCambioFlujo(String strEstadoAnterior, String strEstadoNuevo, String strResponsable) {
        lstFlujoTicket.add(new FlujoTicket(strEstadoAnterior, strEstadoNuevo, strResponsable));
    }

    public void asignarTecnico(Tecnico objTec) {
        if (objTec != null && objTec.tieneCapacidad()) {
            String estadoAnt = this.strEstado;
            this.objTecnicoAsignado = objTec;
            this.strEstado = "Asignado";
            objTec.aumentarCarga();
            
            agregarBitacora("ASIGNACION", "Asignado al técnico " + objTec.getStrNombre());
            registrarCambioFlujo(estadoAnt, "Asignado", objTec.getStrNombre());
        }
    }

    public void resolver(String strSolucion) {
        if (!"Asignado".equalsIgnoreCase(this.strEstado)) {
            throw new IllegalStateException("El ticket debe estar en estado Asignado.");
        }
        String estadoAnt = this.strEstado;
        this.strEstado = "Resuelto";
        
        agregarBitacora("RESOLUCION", "Solución: " + strSolucion);
        registrarCambioFlujo(estadoAnt, "Resuelto", (objTecnicoAsignado != null ? objTecnicoAsignado.getStrNombre() : "Técnico"));
    }

    public void cerrar() {
        if (!"Resuelto".equalsIgnoreCase(this.strEstado)) {
            throw new IllegalStateException("El ticket debe estar Resuelto para cerrarse.");
        }
        String estadoAnt = this.strEstado;
        this.strEstado = "Cerrado";
        if (objTecnicoAsignado != null) objTecnicoAsignado.reducirCarga();
        
        agregarBitacora("CIERRE", "Ticket validado y cerrado.");
        registrarCambioFlujo(estadoAnt, "Cerrado", objSolicitante.getStrNombre());
    }

    public void escalar() {
        if ("Cerrado".equalsIgnoreCase(this.strEstado)) {
            throw new IllegalStateException("No se puede escalar un ticket cerrado.");
        }
        this.blnEscalado = true;
        this.strPrioridad = "Crítica";
        agregarBitacora("ESCALADO", "Escalado a máxima prioridad.");
    }

    public int getIntIdTicket() { return intIdTicket; }
    public String getStrEstado() { return strEstado; }
    public List<Bitacora> getLstBitacora() { return lstBitacora; }
    public List<FlujoTicket> getLstFlujoTicket() { return lstFlujoTicket; }
}