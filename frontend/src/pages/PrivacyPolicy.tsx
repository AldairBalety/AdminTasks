
import React from 'react';

const PrivacyPolicy: React.FC = () => {
  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Política de Privacidad - Digital Pulse</h1>
      <p><strong>Última actualización:</strong> {new Date().toLocaleDateString('es-ES')}</p>
      
      <h2>1. Información que Recopilamos</h2>
      <p>Digital Pulse es un bot automatizado de Facebook que genera y publica contenido sobre tecnología. Recopilamos la siguiente información:</p>
      <ul>
        <li><strong>Información de Facebook:</strong> ID de usuario, nombre público, y datos de interacción con nuestras publicaciones</li>
        <li><strong>Datos de Uso:</strong> Interacciones con nuestro contenido (likes, comentarios, compartidos)</li>
        <li><strong>Información Técnica:</strong> Logs de sistema para el funcionamiento del bot</li>
      </ul>

      <h2>2. Cómo Usamos tu Información</h2>
      <p>Utilizamos la información recopilada para:</p>
      <ul>
        <li>Generar contenido relevante sobre tecnología e innovación</li>
        <li>Mejorar la calidad y relevancia de nuestras publicaciones</li>
        <li>Analizar el engagement y preferencias de nuestra audiencia</li>
        <li>Cumplir con las políticas de Facebook y regulaciones aplicables</li>
      </ul>

      <h2>3. Compartir Información</h2>
      <p>No vendemos, comercializamos ni transferimos tu información personal a terceros, excepto en los siguientes casos:</p>
      <ul>
        <li>Cuando sea requerido por ley o proceso legal</li>
        <li>Para proteger nuestros derechos, propiedad o seguridad</li>
        <li>Con proveedores de servicios que nos ayudan a operar (como OpenAI para generación de contenido)</li>
      </ul>

      <h2>4. Retención de Datos</h2>
      <p>Conservamos tu información solo durante el tiempo necesario para cumplir con los propósitos describidos en esta política, o según lo requiera la ley.</p>

      <h2>5. Seguridad</h2>
      <p>Implementamos medidas de seguridad técnicas y organizativas apropiadas para proteger tu información contra acceso no autorizado, alteración, divulgación o destrucción.</p>

      <h2>6. Tus Derechos</h2>
      <p>Tienes derecho a:</p>
      <ul>
        <li>Acceder a tu información personal</li>
        <li>Corregir información inexacta</li>
        <li>Solicitar la eliminación de tus datos</li>
        <li>Limitar el procesamiento de tu información</li>
        <li>Retirar tu consentimiento en cualquier momento</li>
      </ul>

      <h2>7. Cookies y Tecnologías Similares</h2>
      <p>Utilizamos cookies y tecnologías similares para mejorar la funcionalidad de nuestros servicios y analizar el uso de nuestro contenido.</p>

      <h2>8. Cambios a esta Política</h2>
      <p>Podemos actualizar esta política ocasionalmente. Te notificaremos sobre cambios significativos publicando la nueva política en esta página.</p>

      <h2>9. Contacto</h2>
      <p>Si tienes preguntas sobre esta política de privacidad, puedes contactarnos en:</p>
      <ul>
        <li><strong>Email:</strong> privacy@digitalpulse.com</li>
        <li><strong>Página de Facebook:</strong> Digital Pulse</li>
      </ul>

      <p><em>Esta política de privacidad cumple con las regulaciones de GDPR, CCPA y las políticas de Facebook para desarrolladores.</em></p>
    </div>
  );
};

export default PrivacyPolicy;
